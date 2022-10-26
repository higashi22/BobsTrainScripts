using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    AudioSource Audio;
    public GameObject PanA;
    public GameObject PanB;
    public GameObject PanC;
    public GameObject PanD;
    public GameObject Ob;

    public AudioClip ClickSE;
    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        PanA.SetActive(true);
        PanB.SetActive(false);
        PanC.SetActive(false);
        PanD.SetActive(false);
        Ob.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        Audio.PlayOneShot(ClickSE);
        SceneManager.LoadScene("Main");
    }

    public void HTP()
    {
        Audio.PlayOneShot(ClickSE);
        PanA.SetActive(false);
        PanB.SetActive(true);
        PanC.SetActive(false);
        PanD.SetActive(false);
        Ob.SetActive(false);
    }
    public void HTP2()
    {
        Audio.PlayOneShot(ClickSE);
        PanA.SetActive(false);
        PanB.SetActive(false);
        PanC.SetActive(true);
        PanD.SetActive(false);
        Ob.SetActive(true);
    }
    public void HTP3()
    {
        Audio.PlayOneShot(ClickSE);
        PanA.SetActive(false);
        PanB.SetActive(false);
        PanC.SetActive(false);
        PanD.SetActive(true);
        Ob.SetActive(false);
    }

    public void StartMenu()
    {
        Audio.PlayOneShot(ClickSE);
        PanA.SetActive(true);
        PanB.SetActive(false);
        PanC.SetActive(false);
        PanD.SetActive(false);
        Ob.SetActive(false);
    }
}
