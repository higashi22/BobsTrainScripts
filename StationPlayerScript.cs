using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StationPlayerScript : MonoBehaviour
{
    public static bool isPlaying;
    public FixedJoystick idou;
    public GameObject stick;
    public GameObject Camera;
    public GameObject spot;
    public GameObject cleartext;
    public float speed = 3.0f;
    Animator animator;
    AudioSource Audio;
    public AudioClip TicketSE;
    public AudioClip TouchSE;
    public AudioClip GateSE;
    public AudioClip ClearSE;
    Vector3 Pos;

    Quaternion qqq;
    Quaternion rot;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        Audio = this.gameObject.GetComponent<AudioSource>();
        cleartext.SetActive(false);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == true)
        {
            Move();
        }
    }
    void Move()
    {
        float dx = idou.Horizontal;
        float dy = idou.Vertical;

        float rad = Mathf.Atan2(dx - 0, dy - 0);
        float deg = rad * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y + deg, 0);

        if (deg != 0)
        {
            animator.SetBool("Run", true);
            this.transform.position += this.transform.forward * speed * Time.deltaTime;
            qqq = gameObject.transform.rotation;

        }
        else
        {
            animator.SetBool("Run", false);

            this.transform.rotation = Quaternion.Euler(0, qqq.eulerAngles.y, 0);
            rot = gameObject.transform.rotation;
        }
    }
   
    void OnCollisionEnter(Collision other)
	    {
            if (other.gameObject.tag == "ticket")
            {
                StationManagerScript.pass = 1;
                Audio.PlayOneShot(TicketSE);
            }
            if (other.gameObject.tag == "KS")
            {
                Audio.PlayOneShot(GateSE);
            }
	    }

	    void OnTriggerEnter(Collider other)
	    {
            if (other.gameObject.tag == "stair")
            {
                speed = 8;
            }

            if (other.gameObject.tag == "KS")
            { 
                Audio.PlayOneShot(TouchSE);
            }

        if (other.gameObject.tag == "goal")
        {
            StationManagerScript.isPlaying = false;
                agent.enabled = false;
                Pos =this.transform.position;
                Camera.transform.localPosition = new Vector3(0, 1.5f, 2.5f);
                Camera.transform.Rotate(0f, -180f, 0f);
                cleartext.SetActive(true);
                spot.transform.position = new Vector3(Pos.x, 5.7f, Pos.z + 0.5f);
                animator.SetTrigger("dance");
                Audio.PlayOneShot(ClearSE);
                PlayerPrefs.SetFloat("Score", StationManagerScript.Stimer);
                Debug.Log(StationManagerScript.Stimer);
                Invoke("MvClear", 7f);
        }
	    }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "stair")
            {
                speed = 3;
            }
        }

    void MvClear()
    {
        isPlaying = false;
        SceneManager.LoadScene("ClearScene");
    }

}

