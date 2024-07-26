using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "wave_", menuName = "Data/WaveSpawn")]
public class WaveData : ScriptableObject

{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Vector3[] spawnPositions;
    [SerializeField] int numberOfWave;
    [SerializeField] float timeInteval;
    [SerializeField] float spawnRadius; 

    public GameObject[] EnemyPrefabs => enemyPrefabs;
    public Vector3[] SpawnPosition => spawnPositions;
    public int NumberOfWave => numberOfWave;
    public float TimeInterval => timeInteval;
    public float SpawnRadius => spawnRadius; 

}
