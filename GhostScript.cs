using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostScript : MonoBehaviour
{
    float Gtimer;
    public Text Gtimertext;
    int count = 30;
    public Text countText;
    bool isPlaying;
    AudioSource Audio;
    public AudioClip SuzuSE;
    // Start is called before the first frame update
    void Start()
    {
        Gtimer = PlayerPrefs.GetFloat("TIMER");
        Debug.Log(Gtimer);
        isPlaying = true;
        Audio = this.GetComponent<AudioSource>();
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
            Gtimer -= Time.deltaTime;
            Gtimertext.text = Gtimer.ToString("f1");
        }

        if (Gtimer < 0)
        {
            Gtimer = 0;
            Gtimertext.text = Gtimer.ToString();
            isPlaying = false;
        }
    }

    public void CountDown()
    {
        if (isPlaying == true)
        {
            Audio.PlayOneShot(SuzuSE);
            count--;
            countText.text = count.ToString();
            Debug.Log(count);
        }

        if (count <= 0)
        {
            count = 0;
            countText.text = "FINISH";
            PlayerPrefs.SetFloat("TIMER", Gtimer);
            Debug.Log(Gtimer);
            SceneManager.LoadScene("Main");
        }
    }
}
