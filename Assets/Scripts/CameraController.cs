using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform carTransformMustang;
    public Transform carTransformPickup;
    private Transform carTransformCurrent;
    private GameObject car;

    public Rigidbody carRbMustang;
    public Rigidbody carRbPickup;
    private Rigidbody carRbCurrent;

    public GameManager gameManagerSc;

    private Rigidbody cameraRB;
    
    public Vector3 Offset;
    private Vector3 playerForward;
    private Vector3 carLocalVelocity;
    private float carLocalVelocityZ;

    public float sensitivity = 150f;
    private float xRotation;
    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        car = GameObject.FindGameObjectWithTag("Car");
        carTransformCurrent = car.transform;
        carRbCurrent = car.GetComponent<Rigidbody>();

        cameraRB = carRbCurrent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carLocalVelocity = transform.InverseTransformDirection(carRbCurrent.velocity);
        carLocalVelocityZ = carLocalVelocity.z;

        playerForward = (cameraRB.velocity + carTransformCurrent.transform.forward).normalized;
        
        transform.position = Vector3.Lerp(transform.position,
            carTransformCurrent.position + carTransformCurrent.transform.TransformVector(Offset)
            + playerForward * (-5f),
            carLocalVelocityZ * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {   
            // Get the mouse movements which made at x and y axes.
            mouseX = Input.GetAxisRaw("Mouse X") *Time.deltaTime * sensitivity;
            mouseY = Input.GetAxisRaw("Mouse Y") *Time.deltaTime * sensitivity;

            // Keep the mouse movement which made at y axis between predetermined values.
            // We have to assign "mouseY" variable to another variable because the "Mouse Y" input does not take values as we are used to.
            xRotation -= mouseY; // If you put plus instead of minus the character will look the other way.
            //yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            // Rotate the camera according to mouse movement which made at y axis.
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.LookAt(carTransformCurrent);
            transform.Translate(Vector3.right * Time.deltaTime * mouseX);
            
        }else
        {
            transform.LookAt(carTransformCurrent);
        }
    }
}
