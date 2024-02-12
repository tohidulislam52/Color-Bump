using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 lastmousePosition;
    public GameObject Player2;
    [SerializeField] private float sensitivity,clampData,bouns;
    [HideInInspector]
    public bool Canmove,gameover,finish;

    void Awake()
    {
        Getcomponent();
    }
    void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-bouns,bouns),transform.position.y,Mathf.Clamp(transform.position.z,GameObject.Find("ZValus").transform.position.z,1000));

       if(!Canmove && !gameover && !finish)
       {
            if(Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<GameManager>().RemoveUI();
                Canmove=true;
            }
       }
       if(!Canmove && gameover)
       {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale *.01f;
            }
       }
    }

    void FixedUpdate()
    {
        if(Canmove)
        {
            transform.position += FindObjectOfType<CameraMovement>().cameravalu;

            if(Input.GetMouseButtonDown(0))
            {
                lastmousePosition = Input.mousePosition;
            }

            if(Input.GetMouseButton(0))
            {
                Vector3 vector = lastmousePosition - Input.mousePosition;
                lastmousePosition = Input.mousePosition;
                vector = new Vector3(vector.x,0,vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector,clampData);
                rb.AddForce(/*Vector3.forward *2 + */(-moveForce * sensitivity - rb.velocity/5)
                                            ,ForceMode.VelocityChange);
                // Debug.Log("hi");

            }
            rb.velocity.Normalize();
        }
    }


    private void Getcomponent()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void GameOver()
    {
        GameObject satterspear = Instantiate(Player2,transform.position,Quaternion.identity);
        foreach (Transform item in satterspear.transform)
        {
            item.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5,ForceMode.Impulse);
        }
        Canmove = false;
        gameover = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Time.timeScale = .2f;
        Time.fixedDeltaTime = Time.timeScale *.02f;
        
    }
    IEnumerator NexLevel()
    {
        Canmove = false;
        finish = true;
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
        yield return new WaitForSeconds(1);
        if(PlayerPrefs.GetInt("Level") >= 4)
            SceneManager.LoadScene(Random.Range(0,4));
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    void OnCollisionEnter(Collision terget)
    {
        if(terget.gameObject.tag == "Enemy")
        {
            if(!gameover)
            GameOver();
            
        }
    }
    void OnTriggerEnter(Collider terget)
    {
        if(terget.gameObject.tag == "Finish")
        {
            StartCoroutine(NexLevel());
        }
    }

}
