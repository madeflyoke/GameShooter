using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    //public GameObject enemyPrefabClone { get; private set; }
    private void Start()
    {
        EventManager.EnemyDieEvent += SpawnEnemy;
        SpawnEnemy();
    }
    void Update()
    {
       
    }
    private Vector3 RandomSpawnPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 18f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 18f, NavMesh.AllAreas);
        return hit.position;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, RandomSpawnPoint(), Quaternion.identity);
    }
}
