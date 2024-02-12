using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     private Text cLevelText, nLevelText;
    private Slider fill;

    private int level;
    private float startDistance, distance;

    private GameObject player, finish, hand;
    private TextMesh levelNo;

    void Awake()
    {
        cLevelText = GameObject.Find("CurrentLevelText").GetComponent<Text>();
        nLevelText = GameObject.Find("NextLevelText").GetComponent<Text>();
        fill = GameObject.Find("Fill").GetComponent<Slider>();

        player = GameObject.Find("Player");
        finish = GameObject.Find("Finish");
        hand = GameObject.Find("Hand");

        levelNo = GameObject.Find("LevelNo").GetComponent<TextMesh>();
    }

    void Start()
    {
        level = PlayerPrefs.GetInt("Level");

        levelNo.text = "LEVEL " + level;

        nLevelText.text = (level + 1).ToString();
        cLevelText.text = level.ToString();

        startDistance = Vector3.Distance(player.transform.position, finish.transform.position);

        //SceneManager.LoadScene("Level" + level);
    }

    private void Update()
    {
        // distance = Vector3.Distance(player.transform.position, finish.transform.position);
        // if(player.transform.position.z < finish.transform.position.z)
        //     fill.fillAmount = 1 - (distance / startDistance);

        if(player.transform.position.z < finish.transform.position.z)
        {
            fill.value = player.transform.position.z / finish.transform.position.z;
        }
    }

    public void RemoveUI()
    {
        hand.SetActive(false);
    }
}
