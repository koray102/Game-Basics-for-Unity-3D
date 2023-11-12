using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMainMenu : MonoBehaviour
{
    public Transform targetPos;
    public float speed;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed);

        if(Mathf.Abs(Vector3.Distance(targetPos.position, transform.position)) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
