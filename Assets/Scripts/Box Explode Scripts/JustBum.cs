using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustBum : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject hitObject;
    private PlayerMovementPhysics playerMoveSc;
    public float collisionMultp;
    //public AudioSource partShootSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMoveSc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        hitObject = playerMoveSc.shootedObject;

        if(hitObject == gameObject)
        {
            //partShootSFX.PlayOneShot(partShootSFX.clip);
            rb.AddExplosionForce(collisionMultp, playerMoveSc.hit.point, 2);
        }
    }
}
