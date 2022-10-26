using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManegerScript : MonoBehaviour
{
    // Start is called before the first frame update
    static bool isPlaying = false;
    public float timer;
    public Text timerText;
    public GameObject Player;
    PlayerScript PS;
    void Start()
    {
        PS = Player.GetComponent<PlayerScript>();
        if (PlayerScript.isPlaying == false)
        {
            timer = 180.0f;
            PlayerPrefs.SetFloat("TIMER", timer);
        }
        else
        {
            timer = PlayerPrefs.GetFloat("TIMER");
            Debug.Log(timer);
        }

        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (isPlaying == true)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("f1");
        }

            if (timer < 0)
        {
                timer = 0;
                timerText.text = timer.ToString();
                isPlaying = false;
                PlayerScript.isPlaying = false;
                SceneManager.LoadScene("GameOverScene");
        }
       
    }
}
