using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Pathfinding zombiePath;
    [SerializeField] Transform zombieStartPos;
     

    private void Awake()
    {
        zombiePath = GameObject.Find("A*").GetComponent<Pathfinding>(); 
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
