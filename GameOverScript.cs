using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    AudioSource Audio;
    public AudioClip ClickSE;
    public AudioClip GameOverSE;
    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
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
