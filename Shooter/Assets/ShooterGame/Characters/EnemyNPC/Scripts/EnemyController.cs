using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float ViewRadius = 10f;
    [SerializeField] private float TurnSpeed = 5f;
    [SerializeField] private int MaxEnemyHealth = 100;
    [SerializeField] private int EnemyDamage = 10;
    public int enemyDamage { get => EnemyDamage; }
    [SerializeField] private GameObject enemyAttackPoint;
    [SerializeField] private float closeAttackRange;
    [SerializeField] private ParticleSystem dmgEffect;
    private int currentEnemyHealth;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody [] childsRigidbodies;
    
    private void OnDisable()
    {
        EventManager.ShotEnemyEvent -= GotShot;
        animator.enabled = false;
        Destroy(gameObject, 5f);
        EventManager.CallOnEnemyDie();
    }

    private void Awake()
    {
        enabled = true;
        animator = GetComponent<Animator>();
        animator.enabled = true;
        player =  GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();        
        EventManager.ShotEnemyEvent += GotShot;
        childsRigidbodies = GetComponentsInChildren<Rigidbody>();
        currentEnemyHealth = MaxEnemyHealth;
        
    }
    private void Update()
    {      
        float distanceEnemyPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceEnemyPlayer <= ViewRadius||currentEnemyHealth<MaxEnemyHealth)
        {                
            FaceOnTarget();
            EnemyChasing();
            if (distanceEnemyPlayer <= agent.stoppingDistance+closeAttackRange)
            {
                animator.SetBool("IsInAttackRange", true);             
            }           
        }
        else
        {
            animator.SetBool("IsChasing", false);
            agent.isStopped = true;
        }
    }

    private void FaceOnTarget()
    {
        Vector3 destination = (player.position - transform.position).normalized;
        Quaternion newRot = Quaternion.LookRotation(new Vector3(destination.x, 0f, destination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * TurnSpeed);
    }
    private void EnemyChasing()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetBool("IsChasing", true);
        animator.SetBool("IsInAttackRange", false);     
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRadius);
    }

    private void EnemyAttack() //attack animation event
    {
        if (!enemyAttackPoint.activeInHierarchy)
        {
            enemyAttackPoint.SetActive(true);
        }
        else
        {
            enemyAttackPoint.SetActive(false);
        }
        
    }

    private void GotShot(int damage, RaycastHit target) 
    {      
        if (childsRigidbodies.Contains(target.collider.attachedRigidbody))
        {  
            currentEnemyHealth -= damage;
            ParticleSystem tmpHit = Instantiate(dmgEffect, target.point, Quaternion.identity/*target.rigidbody.gameObject.transform*/);
            Destroy(tmpHit.gameObject, 1f);
            if (currentEnemyHealth<=0)
            {              
                foreach (var item in childsRigidbodies)
                {
                    item.isKinematic = false;
                }
                enabled = false;
            }           
        }
        
    }

}
