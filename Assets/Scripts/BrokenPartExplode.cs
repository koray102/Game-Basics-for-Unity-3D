using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPartExplode : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject hitObject;
    private PlayerMovementPhysics playerMoveSc;
    public float collisionMultp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMoveSc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementPhysics>();
    }


    void Update()
    {
        //hitObject = playerMoveSc.shootedObject;
    }


    void FixedUpdate()
    {
        if(hitObject == gameObject)
        {
            //partShootSFX.PlayOneShot(partShootSFX.clip);
            //rb.AddExplosionForce(collisionMultp, playerMoveSc.hit.point, 2);
        }
    }
}
