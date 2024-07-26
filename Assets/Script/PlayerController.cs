using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float jumpForce;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos; 
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxHealth=100;
    [SerializeField] float takeDamage = 5.0f;
    [SerializeField] float currentHealth;
    [SerializeField] bool gameOver = false; 
    private Rigidbody rb;
    private Animator animiationControll;
    [SerializeField] bool isOnGround = false;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool GameOver => gameOver; 
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
        rb = GetComponent<Rigidbody>();
        animiationControll = GetComponent<Animator>();
        //bulletPrefab = GetComponent<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isOnGround ==true && gameOver ==false)
        {
            PlayerMovement();
            PlayerAnimationControll();
        }
       
    }

    void PlayerAnimationControll()
    {
        if (rb.velocity.magnitude>0f && isOnGround==true)
        {
            animiationControll.SetTrigger("run");
        }

        if (rb.velocity.magnitude ==0 && isOnGround ==true)
        {
            animiationControll.SetTrigger("stop_running");
        }

        if (gameOver == true && isOnGround ==true)
        {
            animiationControll.SetTrigger("die");
        }

        if (isOnGround == false)
        {
            animiationControll.SetTrigger("not_on_ground"); 
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("zombie"))
        {
            currentHealth -= takeDamage; 

            if (currentHealth <0)
            {
                gameOver = true; 
            }
        }
        
    }

    void ShootOnTargetDetection()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit; 
        if (Physics.Raycast (ray, out hit, 400f))
        {
            Debug.Log("using for shooting");
        }
    }
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");
        float turn = rotate * rotateSpeed * Time.deltaTime;
        Vector3 horizontalMove = Vector3.right * horizontal;
        Vector3 verticalMove = Vector3.forward * vertical; 
        transform.Translate(verticalMove * Time.deltaTime * speed);
        transform.Translate(horizontalMove * Time.deltaTime * speed); 
        transform.Rotate(Vector3.up * turn);

        if (Input.GetMouseButton(0))
        {
            Instantiate(bulletPrefab, bulletPos.position, transform.rotation);
            animiationControll.SetTrigger("shoot");
        }

        else animiationControll.SetTrigger("stop_shooting");

    }    
}
