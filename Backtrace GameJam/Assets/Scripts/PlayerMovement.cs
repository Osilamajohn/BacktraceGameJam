using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform groundCheck;
    private bool isGrounded;
    public float radius;
    public LayerMask groundMask;

    public float speed;
    public float jumpSpeed;
    public float seconds;

    Animator animator;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 playerMov = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;

        transform.Translate(playerMov, Space.Self);

        Debug.Log(isGrounded);

        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody.velocity = new Vector3(0, jumpSpeed, 0);
            animator.SetTrigger("jump");
            StartCoroutine(jumpTime());
        }



        if(playerMov.magnitude == 0)
        {
            animator.SetBool("walking", false);
        } else
        {
            animator.SetBool("walking", true);
           
        }



        if (playerMov.magnitude == 0)
        {
            animator.SetBool("runing", false);
            speed = 5;
        } else if (playerMov.magnitude != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("runing", true);
            animator.SetBool("walking", false);
            speed = 10;
        } else
        {
            speed = 5;
            animator.SetBool("runing", false);
        }


        



    }


    IEnumerator jumpTime()
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<Rigidbody>().useGravity = true;
    }



}

