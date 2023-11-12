using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGame : MonoBehaviour
{
    public float spawnInterval1;
    public float spawnInterval2;
    public float spawnInterval3;
    public float spawnInterval4;

    public GameObject dronePrefab;
    private GameObject instantiated;

    public Transform startPos1;
    public Transform startPos2;
    public Transform startPos3;
    public Transform startPos4;

    public Transform endPos1;
    public Transform endPos2;
    public Transform endPos3;
    public Transform endPos4;
    

    void Start()
    {
        StartCoroutine(DroneSpawn1(spawnInterval1));
        StartCoroutine(DroneSpawn2(spawnInterval2));
        StartCoroutine(DroneSpawn3(spawnInterval3));
        StartCoroutine(DroneSpawn4(spawnInterval4));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DroneSpawn1(float spawnInterval)
    {
        while(true)
        {
            Debug.Log("1");
            instantiated = Instantiate(dronePrefab, startPos1.position, startPos1.rotation);
            instantiated.GetComponent<DroneMainMenu>().targetPos = endPos1;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator DroneSpawn2(float spawnInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
        
            Debug.Log("2");
            instantiated = Instantiate(dronePrefab, startPos2.position, startPos2.rotation);
            instantiated.GetComponent<DroneMainMenu>().targetPos = endPos2;
        }
    }

    IEnumerator DroneSpawn3(float spawnInterval)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
        
            Debug.Log("3");
            instantiated = Instantiate(dronePrefab, startPos3.position, startPos3.rotation);
            instantiated.GetComponent<DroneMainMenu>().targetPos = endPos3;
        }
    }

    IEnumerator DroneSpawn4(float spawnInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
        
            Debug.Log("4");
            instantiated = Instantiate(dronePrefab, startPos4.position, startPos4.rotation);
            instantiated.GetComponent<DroneMainMenu>().targetPos = endPos4;
        }
    }
}
