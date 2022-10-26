using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearScenScript : MonoBehaviour
{
    AudioSource Audio;
    public AudioClip ClickSE;
    float LtScore;
    public Text LtScoreText;
    float HiScore;
    public Text HiScoreText;
    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        LtScore = PlayerPrefs.GetFloat("Score");
        LtScore = 180.0f - LtScore;
        LtScoreText.text = LtScore.ToString("f1");
        HiScore = PlayerPrefs.GetFloat("HighScore");

        if (HiScore > 0)
        {
            if (HiScore > LtScore)
            {
                HiScore = LtScore;
                PlayerPrefs.SetFloat("HighScore", HiScore);
            }
        }
        else
        {
            HiScore = LtScore;
            PlayerPrefs.SetFloat("HighScore", HiScore);
        }

        HiScoreText.text = HiScore.ToString("f1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Replay()
    {
        Audio.PlayOneShot(ClickSE);
        SceneManager.LoadScene("Main");
    }

    public void StartMenu()
    {
        Audio.PlayOneShot(ClickSE);
        Invoke("MvS", 0.5f);
    }

    void MvS()
    {
        SceneManager.LoadScene("StartScene");
    }
}