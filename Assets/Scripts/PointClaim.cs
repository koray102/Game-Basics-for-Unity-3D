using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PointClaim : MonoBehaviour
{
    public float rotationSpeed;
    public AudioSource PointClaimSFX;
    private bool isPointClaimed = false;
    public GameObject effect;
    public float knockBackPowerUp;
    private MeshRenderer pointMesh;
    private GameObject player;
    private PlayerMovementPhysics playerMovementSc;


    void Start()
    {
        pointMesh =gameObject.GetComponent<MeshRenderer>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovementSc = player.GetComponent<PlayerMovementPhysics>();
    }


    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }


    private  IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") & !isPointClaimed)
        {
            PointClaimSFX.Play();
            playerMovementSc.knockBackPower = knockBackPowerUp;
            isPointClaimed = true;
            
            Destroy(pointMesh);
            Destroy(effect);
            
            yield return new WaitForSeconds(PointClaimSFX.clip.length);
            Destroy(transform.parent.gameObject);
        }
    }
}
