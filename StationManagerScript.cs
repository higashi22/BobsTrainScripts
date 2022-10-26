using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StationManagerScript : MonoBehaviour
{
    public GameObject ticketWall;
    Collider wall;
    public static int pass;
    public GameObject card;
    public static bool isPlaying = false;
    public static float Stimer;
    public Text StimerText;
    // Start is called before the first frame update
    void Start()
    {
        Stimer = PlayerPrefs.GetFloat("MainTimer");
        pass = PlayerPrefs.GetInt("PASS");
        wall = ticketWall.GetComponent<Collider>();
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        Pass();
        Timer();
    }

    void Pass()
    {
        if (pass == 1)
        {
            card.SetActive(true);
            wall.isTrigger = true;
            Debug.Log("T");
        }
        else
        {
            card.SetActive(false);
            wall.isTrigger = false;
            Debug.Log("F");
        }
    }
    void Timer()
    {
        if (isPlaying == true)
        {
            Stimer -= Time.deltaTime;
            StimerText.text = Stimer.ToString("f1");
        }

        if (Stimer < 0)
        {
            StationPlayerScript.isPlaying = false;
            Stimer = 0;
            StimerText.text = Stimer.ToString();
            isPlaying = false;
            SceneManager.LoadScene("GameOverScene");
        }

    }

}
