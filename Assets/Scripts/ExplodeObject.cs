using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    public float collisionMultp;
    public AudioSource boxExplodeSFX;
    private PlayerMovementPhysics playerMoveSc;
    private bool didExploded = false;
    private GameObject childObj;
    public float BrokenPartCollMultp;


    public void Explode()
    {
        playerMoveSc = GameObject.FindWithTag("Player").GetComponent<PlayerMovementPhysics>();

        if(!didExploded)
        {
            boxExplodeSFX.PlayOneShot(boxExplodeSFX.clip);

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            
            foreach (var childRbs in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                childObj = childRbs.gameObject;
                if(childObj != gameObject)
                {
                    childObj.AddComponent<MeshCollider>();
                    childObj.GetComponent<MeshCollider>().convex = true;

                    childObj.AddComponent<BrokenPartExplode>();
                    childObj.GetComponent<BrokenPartExplode>().collisionMultp = BrokenPartCollMultp;

                    childRbs.isKinematic = false;
                    childRbs.AddExplosionForce(collisionMultp, playerMoveSc.hit.point, 2000);
                }
            }

            didExploded = true;
        }
    }
}
