using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<uint> WavesEnemiesNumber;
    private int CurrentWaveIndex=0;

    private void Start()
    {
        EventManager.EnemyDieEvent += EnemyDied;
        if (WavesEnemiesNumber.Count!=0)
            SpawnEnemyWave(WavesEnemiesNumber[CurrentWaveIndex]);       
    }
    private void OnDisable()
    {
        EventManager.EnemyDieEvent -= EnemyDied;
    }

    private void Update()
    {
       
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
            Debug.Log(CurrentWaveIndex + " WORK");
            CurrentWaveIndex++;
            Debug.Log(CurrentWaveIndex + " WORK");
            StartCoroutine(NextWave());            
        }

    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(5f);
        SpawnEnemyWave(WavesEnemiesNumber[CurrentWaveIndex]);     
    }

    private void SpawnEnemyWave(uint EnemiesNumber)
    {
        while (EnemiesNumber > 0)
        {
            Instantiate(enemyPrefab, RandomSpawnPoint(), Quaternion.identity);
            EnemiesNumber--;
        }
    }
}
