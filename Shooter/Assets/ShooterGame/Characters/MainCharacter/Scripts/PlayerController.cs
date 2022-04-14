using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RepositoryBase repositoryBase;
    [SerializeField] private AudioClip playerGetDmgSFX;
    [SerializeField] private int maxHealth;
    
    private AudioSource audioSource;
    private int enemyDamage = 0;
    private int currentHealth;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        repositoryBase.PlayerInfoObj.MaxHealth = maxHealth;
    }
    private void Start()
    {
        currentHealth = repositoryBase.PlayerInfoObj.MaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitPoint"))
        {
            if (enemyDamage == 0)
            {
                enemyDamage = other.gameObject.GetComponentInParent<EnemyController>().enemyDamage;
            }
            currentHealth -= enemyDamage;
            EventManager.CallOnPlayerHit(enemyDamage, currentHealth);
            audioSource.PlayOneShot(playerGetDmgSFX);
            if (currentHealth<=0)
            {
                EventManager.CallOnEndGameEvent();
            }
        }
        
    }

}
