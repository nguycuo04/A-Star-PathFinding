using System.Collections;
using System.Collections.Generic;
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
    //[SerializeField] float outOfBoundX = 50.0f ;
    //[SerializeField] float outOfBoundZ= 50.0f; 
    private Rigidbody rb;
    private Animator animiationControll;
    [SerializeField] bool isOnGround = false;
    //public bool onZombieAttach = false; 

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animiationControll = GetComponent<Animator>();
        //bulletPrefab = GetComponent<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isOnGround ==true)
        {
            PlayerMovement();
            PlayerAnimationControll();
        }
       
    }

    void PlayerAnimationControll()
    {
        if (rb.velocity.magnitude>0f)
        {
            animiationControll.SetTrigger("run");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }
        
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.CompareTag("zombie"))
    //    {
    //        onZombieAttach = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("zombie"))
    //    {
    //        onZombieAttach = false;
    //    }
    //}
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
        transform.Rotate(Vector3.up * turn);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(bulletPrefab, bulletPos.position, transform.rotation);
            //if (bulletPrefab.transform.position.x < -outOfBoundX || bulletPrefab.transform.position.x > outOfBoundX
            //    || bulletPrefab.transform.position.z < -outOfBoundZ || bulletPrefab.transform.position.z > outOfBoundZ)
            //    {
            //        Destroy(gameObject); 
            //    }
        }

    }    
}
