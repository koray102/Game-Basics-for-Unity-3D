using System;
using UnityEngine;


public class ObjectFollower : MonoBehaviour
{
     // Hedef nesnenin transformu
    public float moveSpeed = 5f;  // Hareket hýzý
    public LayerMask obstacleLayer;  // Duvarýn katmaný



    public Animator animator;
    public  Transform[] target;
    public Transform secilmis_target;
    public string secilmis_tag = "hedef";
    private int index = 0;
    private float zýplama_gucu = 5;

    public bool robot_win = false;

    void Start()
    {
        
        
        
       
    }



    private bool isClimbing = false;  // Týrmanma durumu
    private float climbTimer = 0f;  // Týrmanma süresi
    private float climbDelay = 1f;  // Týrmanma gecikmesi

    void Update()
    {
        if (target != null)
        {
            // Hedefe doðru yönelme
            Vector3 direction = (secilmis_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // Hedefe doðru ilerleme
            if (!isClimbing)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, obstacleLayer))
                {
                    // Duvar tespit edildi, týrmanma baþlat
                    Debug.Log(132131231313);
                    isClimbing = true;
                    climbTimer = 0f;
                }
                else
                {
                    transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                }
            }
            else
            {
                // Týrmanma durumu
                climbTimer += Time.deltaTime;

                if (climbTimer >= climbDelay)
                {
                    // Týrmanma gecikmesi sona erdi, týrmanma durumunu sýfýrla
                    isClimbing = false;
                    animator.SetBool("Jump", false);
                    zýplama_gucu += 0.25f;
                }
                else
                {
                    // Týrmanma süreci
                    transform.Translate(Vector3.up * moveSpeed * zýplama_gucu * Time.deltaTime, Space.World);
                    animator.SetBool("Jump", true);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {   
        other.enabled = false;
        if (other.CompareTag("hedef"))
        {

            secilmis_target = target[1];
        }else if (other.CompareTag("hedef1"))
        {
            secilmis_target = target[2];
        }
        else if (other.CompareTag("hedef2"))
        {
            secilmis_target = target[3];
        }
        else if (other.CompareTag("hedef3"))
        {
            secilmis_target = target[4];
        }
        else if (other.CompareTag("hedef4"))
        {
            secilmis_target = target[5];
        }
        else if (other.CompareTag("hedef5"))
        {
            secilmis_target = target[6];
        }
        else if (other.CompareTag("hedef6"))
        {
            secilmis_target = target[7];
        }
        else if (other.CompareTag("hedef7"))
        {
            secilmis_target = target[8];
        }



    }
}