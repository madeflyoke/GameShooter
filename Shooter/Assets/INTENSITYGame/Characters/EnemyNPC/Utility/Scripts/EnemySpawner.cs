using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private GameObject enemyPrefab;
    private List<int> WavesEnemiesNumber = new List<int>(SpawnMenuUI.enemiesWavesRef);      
    private int CurrentWaveIndex=0;
    private List<Vector3> spawnPoints = new List<Vector3>();
    private void Start()
    {
        if (WavesEnemiesNumber.Count != 0)
            StartCoroutine(SpawnEnemyWave(WavesEnemiesNumber[CurrentWaveIndex]));
    }
    private void OnEnable()
    {
        EventManager.EnemyDieEvent += EnemyDied;
    }

    private void OnDisable()
    {
        EventManager.EnemyDieEvent -= EnemyDied;
    }

    private Vector3 RandomSpawnPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 18f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 18f, NavMesh.AllAreas);
        return hit.position;
    }

    private void EnemyDied()
    {
        WavesEnemiesNumber[CurrentWaveIndex]--;
        if (WavesEnemiesNumber[CurrentWaveIndex] == 0 && WavesEnemiesNumber.Count > (CurrentWaveIndex + 1))
        {           
            CurrentWaveIndex++;
            StartCoroutine(SpawnEnemyWave(WavesEnemiesNumber[CurrentWaveIndex]));
        }
        if (WavesEnemiesNumber[CurrentWaveIndex] == 0 && WavesEnemiesNumber.Count <= (CurrentWaveIndex + 1))
        {
           StartCoroutine(EndGameDelay());
        }
        
    }
    IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(3f);
        EventManager.CallOnEndGameEvent();
    }
    IEnumerator SpawnEnemyWave(int EnemiesNumber)
    {
        int enemiesNumber = EnemiesNumber;
        while (enemiesNumber > 0)
        {
            Vector3 spawnPoint = RandomSpawnPoint();
            GameObject tmpEffect = Instantiate(spawnEffect, spawnPoint, Quaternion.identity);
            Destroy(tmpEffect,5f);
            spawnPoints.Add(spawnPoint);
            enemiesNumber--;         
        }
        yield return new WaitForSeconds(5f);

        enemiesNumber = EnemiesNumber;
        int spawnPointIndex = 0;
        while (enemiesNumber > 0)
        {          
            Instantiate(enemyPrefab, spawnPoints[spawnPointIndex], Quaternion.identity);
            enemiesNumber--;
            spawnPointIndex++;         
        }
        spawnPoints.Clear();
    }
}
