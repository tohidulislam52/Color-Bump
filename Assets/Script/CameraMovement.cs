using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float CameraSpeed;
    public Vector3 cameravalu;

    void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(FindObjectOfType<PlayerMovement>().Canmove)
        {
            transform.position += Vector3.forward * CameraSpeed * Time.fixedDeltaTime; 
            cameravalu = Vector3.forward * CameraSpeed * Time.fixedDeltaTime;
        }
       
    }
}
