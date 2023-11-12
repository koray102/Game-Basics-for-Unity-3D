using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementPhysics : MonoBehaviour
{
    private float speed;
    public float speedInput;
    public float runSpeed;
    private Rigidbody playerRb;
    private float leftRight;
    private float forwardBackward;
    public float jumpForceInput;
    private float jumpForce;
    private Vector3 direction;
    public float knockBackPower;
    public float Ymultp;
    public float XZmultp;
    public Transform camTransform;
    private int shootCount;
    public GameObject bumBum;
    private GameObject fireEffect;
    public RaycastHit hit;
    internal float timeAfterShoot = 1;
    public float shootDelay;
    private bool isSlow;
    public float slowDownSpeedInput;
    public float slowDownJumpInput;
    public Slider slider;


    private bool isGrounded;
    public Transform groundCheckTransform;
    public float radiusOfSphere = 0.3f;
    public LayerMask groundMask;
    public GameObject shootedObject;
    public AudioSource shootSFX;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, radiusOfSphere, groundMask);
        
        if(isSlow)
        {
            speed = slowDownSpeedInput;
            if(Input.GetKeyDown("space") & isGrounded)
            {
                //Debug.Log("zipla");
                jumpForce = slowDownJumpInput;
            }
        }else
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }else
            {
                speed = speedInput;
            }
            
            if(Input.GetKeyDown("space") & isGrounded)
            {
                //Debug.Log("zipla");
                jumpForce = jumpForceInput;
            }
        }

        leftRight = Input.GetAxis("Horizontal") * speed;
        forwardBackward = Input.GetAxis("Vertical") * speed;

        if(isGrounded)
        {
            shootCount = 0;
        }else
        {
            jumpForce = 0;
        }

       

        //Debug.Log(hit.normal);
        //Debug.DrawRay(hit.point, camTransform.position - hit.point, Color.green);

        if(Input.GetMouseButtonDown(0) & shootCount == 0 & timeAfterShoot > shootDelay)
        {
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, 1<<7))
            {
                
            }else
            {
                shootSFX.PlayOneShot(shootSFX.clip);
                timeAfterShoot = 0;

                if(!isGrounded)
                {
                    shootCount += 1;
                }

                Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity);
                shootedObject = hit.collider.gameObject;
                
                direction = (transform.position - hit.point).normalized * knockBackPower;
                direction = new Vector3(direction.x * XZmultp, direction.y * Ymultp, direction.z * XZmultp);
                //Debug.Log("silahla zipla");
                playerRb.AddForce(direction, ForceMode.Impulse);

                fireEffect = Instantiate(bumBum, hit.point, Quaternion.Euler(hit.normal));
                Destroy(fireEffect, 2);
            }
        }else
        {
            timeAfterShoot += Time.deltaTime;
            if(shootCount == 0)
            {
                slider.value = timeAfterShoot;
            }else
            {
                slider.value = 0;
            }
        }
    }


    void FixedUpdate()
    {
        playerRb.MovePosition(transform.position + Time.fixedDeltaTime * transform.TransformDirection(new Vector3(leftRight, playerRb.velocity.y, forwardBackward)));
    
        playerRb.AddForce(Vector3.up * jumpForce);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("SlowDown"))
        {
            isSlow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("SlowDown"))
        {
            isSlow = false;
        }
    }
}
