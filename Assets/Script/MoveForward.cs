using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    //public bool shotOnTarget = false; 
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireInTheHole();
    }

    void FireInTheHole()
    {
       transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); 

       if ( transform.position.x > 30 || transform.position.x < -30 || transform.position.z > 30 || transform.position.z <-30)
        {
           Destroy(gameObject);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
     
    //    if (other.gameObject.CompareTag("zombie"))
    //    {
    //        shotOnTarget = true;
    //        Debug.Log(" toi roi");
    //        //Destroy(other.gameObject);
    //    }
    //   shotOnTarget = false;
    //}
}
