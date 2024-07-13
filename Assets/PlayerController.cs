using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float jumpForce;
    [SerializeField] float rotateSpeed; 
    private Rigidbody rb; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");
        float turn = rotate * rotateSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Vector3 horizontalMove = Vector3.right * horizontal;
        Vector3 verticalMove = Vector3.forward * vertical; 
        transform.Translate(horizontalMove * Time.deltaTime * speed);
        transform.Translate(verticalMove * Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * turnRotation, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }    
}
