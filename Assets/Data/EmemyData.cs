using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ememydata : ScriptableObject
{
    [CreateAssetMenu(fileName = "enemy_", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
       
        [SerializeField] float maxHp;
        [SerializeField] float speed; 

    }

}
