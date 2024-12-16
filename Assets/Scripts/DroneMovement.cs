using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public float speed;
    public Transform targetPos;
    public GameObject replacement;
    private GameObject replacement1;
    public float collisionMultp;
    public float shootInterval;
    public float shootPower;
    public GameObject fireLeft;
    public GameObject fireRight;
    public GameObject shootVFX;
    private GameObject shootLeftIns;
    private GameObject shootRightIns;
    public Transform shootLeftPoint;
    public Transform shootRightPoint; 
    public GameObject drone;
    public AudioSource exploedSFX;    
    public AudioSource droneShootSFX;

    private PlayerMovementPhysics playerMoveSc;
    private Vector3 distance;
    private GameObject player;
    private Rigidbody playerRb;
    private bool didExploded = false;
    private GameObject hitObject;
    private Rigidbody[] rbs;
    private MeshRenderer[] meshes;
    public RaycastHit hit;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMoveSc = player.GetComponent<PlayerMovementPhysics>();
        playerRb = player.GetComponent<Rigidbody>();

        
        StartCoroutine(Shoot(shootInterval, shootPower));
    }



    void Update()
    {
        transform.LookAt(player.transform);
        transform.position = Vector3.Lerp(transform.position, targetPos.position, speed);

        //hitObject = playerMoveSc.shootedObject;

        if(hitObject == drone & !didExploded)
        {
            exploedSFX.PlayOneShot(exploedSFX.clip);

            fireLeft.SetActive(false);
            fireRight.SetActive(false);

            meshes = drone.GetComponentsInChildren<MeshRenderer>();
            GetComponentInChildren<Collider>().enabled = false;

            foreach (var mesh in meshes)
            {
                mesh.enabled = false;;
            }

            replacement1 = Instantiate(replacement, transform.position, transform.rotation);
            rbs = replacement1.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rbs)
            {
                //rb.AddExplosionForce(collisionMultp, playerMoveSc.hit.point, 2);
            }
            didExploded = true;
            Destroy(gameObject, exploedSFX.clip.length);
        }
    }

    IEnumerator Shoot(float shootInterval, float shootPower)
    {
        while(!didExploded)
        {
            yield return new WaitForSeconds(shootInterval);
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject == player)
                {
                    droneShootSFX.PlayOneShot(droneShootSFX.clip);
                    shootLeftIns = Instantiate(shootVFX, shootLeftPoint.position, shootLeftPoint.rotation);
                    shootRightIns = Instantiate(shootVFX, shootRightPoint.position, shootRightPoint.rotation);

                    Destroy(shootLeftIns, 2);
                    Destroy(shootRightIns, 2);
                    distance = (player.transform.position - transform.position).normalized;

                    Debug.Log("zort");
                    playerRb.AddForce(distance * shootPower);
                }
            }
        }
    }
}
