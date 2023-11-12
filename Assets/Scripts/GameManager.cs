using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.backgroundColor = new Color(value, value, value);
    }
}
