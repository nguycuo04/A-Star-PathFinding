using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class AgentController : MonoBehaviour
{
    private PlayerController player; 
    private NavMeshAgent agent;
    private Rigidbody zombieRb; 
    [SerializeField] ParticleSystem bloodEffect;
    [SerializeField] Animator animator;
    [SerializeField] bool shotOnTarget = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>(); 
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieRb = GetComponent<Rigidbody>();
        //bloodEffect = GetComponent<ParticleSystem>();
        //bloodEffect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        PlayEffectAndAnimation();
        
        if (player.transform.position != null && shotOnTarget !=true)
        {
            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(player.transform.position);
            }
        }

      
    }

    void PlayEffectAndAnimation()
    {
        if ( shotOnTarget ==true )
        {
            
            animator.SetTrigger("dealth");
            StartCoroutine(WaitBeforeDestroy());
        }

        if (zombieRb.velocity.magnitude >0)
        {
            animator.SetTrigger("walk"); 
        }
        //if (playercontroller.onzombieattach == true)
        //{
        //    animator.settrigger("attack");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            shotOnTarget = true;
            bloodEffect.Play();
            agent.enabled = false; 
        }
        Destroy(other.gameObject);

    }

    private void OnTriggerExit(Collider other)
    {
        shotOnTarget = false; 
    }

    IEnumerator WaitBeforeDestroy()
    {
       
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject); 
    }
}


