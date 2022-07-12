using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    Vector3 movement;
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    public bool swingSword;
    Animator armAnim;
    public GameObject arm;
    public bool inChestArea;
    public GameObject chest;
    public bool canJump;
    public GameObject numberObject;
    numberController numScript;
    public bool canSwing = true;
    bool pauseGame;

    void Start()
    {
        armAnim = arm.GetComponent<Animator>();
        numScript = numberObject.GetComponent<numberController>();
    }

    private void FixedUpdate()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        controller.Move(movement * 12 * Time.deltaTime);

        if(canJump)
        {
            canJump = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Journal"))
        {
            pauseGame = !pauseGame;   
        }
        if (numScript.meleeWeapon && Input.GetButtonDown("Swing") && canSwing && !pauseGame)
        {
            canSwing = false;
            armAnim.SetBool("swing", true);
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        movement = transform.right * horizontalInput + transform.forward * verticalInput;

        if(Input.GetButtonDown("Jump") && isGrounded && canJump == false)
        {
            canJump = true;
        }

        /*if(Input.GetButtonDown("swing"))
        {
            //armObjectScript.swing = true;
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chest")
        {
            inChestArea = true;
            chest = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            inChestArea = false;
            chest = null;
        }
    }
}
