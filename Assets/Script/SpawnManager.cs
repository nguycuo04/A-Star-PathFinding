using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
            for (int i = 0; i < waveData.EnemyPrefabs.Length; i++)
            {
                preFabsIndex = Random.Range(0, waveData.EnemyPrefabs.Length);
                spawnIndex = Random.Range(0, waveData.SpawnPosition.Length);
                Instantiate(waveData.EnemyPrefabs[preFabsIndex], waveData.SpawnPosition[spawnIndex], transform.rotation);
            }
            yield return new WaitForSeconds(waveData.TimeInterval);
    }
}
