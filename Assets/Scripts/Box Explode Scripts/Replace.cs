using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replace : MonoBehaviour
{
    public GameObject replacement;
    private GameObject replacement1;
    public float collisionMultp;
    public AudioSource boxExplodeSFX;
    private PlayerMovementPhysics playerMoveSc;
    private bool didExploded = false;
    private GameObject hitObject;
    private Rigidbody[] rbs;
    
    void Start()
    {
        playerMoveSc = GameObject.FindWithTag("Player").GetComponent<PlayerMovementPhysics>();
    }


    void Update()
    {
        hitObject = playerMoveSc.shootedObject;

        if(hitObject == gameObject & !didExploded)
        {
            boxExplodeSFX.PlayOneShot(boxExplodeSFX.clip);

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            replacement1 = Instantiate(replacement, transform.position, transform.rotation);
            rbs = replacement1.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rbs)
            {
                rb.AddExplosionForce(collisionMultp, playerMoveSc.hit.point, 10);
            }
            didExploded = true;
            Destroy(gameObject, boxExplodeSFX.clip.length);
        }
    }
}
