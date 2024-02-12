using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Vector2 MinMax;
    [SerializeField] private float speed;
    private Rigidbody rb;
    private bool Right,left;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        left = true;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.AddForce(Vector3.right*speed*Time.fixedDeltaTime);
        
    }
    void FixedUpdate()
    {    //-3.5 <= 0
        

        if(transform.position.x <= MinMax.x)
        {
            Right = true;
            left = false;
        }
        else if(transform.position.x >= MinMax.y)
        {
            left = true;
            Right = false;
        }

        if(Right)
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.right*Time.fixedDeltaTime*speed);
            transform.Translate(Vector3.right * Time.fixedDeltaTime * speed);
        }
        else if(left)
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.left*speed*Time.fixedDeltaTime);
            transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);

        }
    }
}
