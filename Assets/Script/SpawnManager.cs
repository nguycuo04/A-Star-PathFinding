using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] WaveData waveData;
    [SerializeField] int preFabsIndex;
    [SerializeField] int spawnIndex; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTheEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTheEnemy()
    {
        for(int j = 0; j < waveData.NumberOfWave; j++)
        {
            for (int i = 0; i < waveData.EnemyPrefabs.Length; i++)
            {
                int layerMask = LayerMask.GetMask("unmovable");
                preFabsIndex = Random.Range(0, waveData.EnemyPrefabs.Length);
                spawnIndex = Random.Range(0, waveData.SpawnPosition.Length);

                NavMeshHit hit;

                if (NavMesh.SamplePosition(waveData.SpawnPosition[spawnIndex], out hit, waveData.SpawnRadius , NavMesh.AllAreas))
                {
                    //if (!Physics.CheckSphere(waveData.SpawnPosition[spawnIndex], waveData.SpawnRadius, layerMask))
                
                    Instantiate(waveData.EnemyPrefabs[preFabsIndex], waveData.SpawnPosition[spawnIndex], Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(waveData.TimeInterval);
        }
       
    }
}
