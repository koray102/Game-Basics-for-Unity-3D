using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* To-Do list for use this script
    1) Create a empty object inside the character and put it to the bottom of the character
    2) Add a new layer as "Ground" and set "groundMask" variable to "Ground"
    3) Set obect layers as "Ground" whichever ones are the ground */

    public CharacterController characterController;
    public float speed = 10f;
    public Transform groundCheckTransform;
    public float radiusOfSphere = 0.3f;
    public LayerMask groundMask;
    public float gravity = -50f;
    public float jumpVelocity = 0.2f;
    private float velocityY;
    private bool isGrounded;
    private float xInput;
    private float zInput;
    private Vector3 movementDirectionVector;
    
    void Start()
    {
        
    }

    void Update()
    {
        // WALK
        // Get the "wasd" buttons' inputs.
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        // We have to use "tranform.blabla" otherwise the character will not go in the direction he/she is looking.
        movementDirectionVector = transform.right * xInput + transform.forward * zInput;
        characterController.Move(movementDirectionVector * speed * Time.deltaTime); 
        
        // GRAVITY FORCE
        /* "Physics.CheckSphere" is creating a sphere according to the informations that you have written in parentheses.
        And this sphere checks if it is colliding the object which you have specified by 3. variable or not */
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, radiusOfSphere, groundMask);

        // If you don't use this if statement the "realisticGravity" variable will increase constantly.
        if (isGrounded && velocityY < 0)
        {
            // You can make it "0f" but it is better.
            velocityY = 0f;
        }else
        {
            // To make gravity force more realistic (1/2 * g * t^2)
            velocityY += gravity * Time.deltaTime * Time.deltaTime;
        }
        characterController.Move(new Vector3(0, velocityY , 0));

        //JUMP
        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocityY = jumpVelocity;
        }
    }
}
