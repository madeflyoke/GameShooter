using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float ViewRadius = 10f;
    [SerializeField] private float TurnSpeed = 5f;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody [] childsRigidbodies;

    private void OnDisable()
    {
        animator.enabled = false;
        Destroy(gameObject, 5f);
        EventManager.CallOnEnemyDie();
    }

    private void Awake()
    {
        enabled = true;
        animator = GetComponent<Animator>();
        animator.enabled = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();        
        EventManager.ShotAndHitEvent += GotShot;
        childsRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {      
        float distanceEnemyPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceEnemyPlayer <= ViewRadius)
        {
            FaceOnTarget();
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("IsChasing", true);
            if (distanceEnemyPlayer <= agent.stoppingDistance)
            {            
                animator.SetBool("IsChasing", false);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ViewRadius);
    }

    private void GotShot(float damage)
    {    
        foreach (var item in childsRigidbodies)
        {
            item.isKinematic = false;
        }
        enabled = false;
    
    }



}
