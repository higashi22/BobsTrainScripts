using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{

    Vector3 touchStartPos, touchEndPos;
    float jumpPower = 2.5f;
    float spfirst = 15.0f;
    public float speed;
    string direction;
    float dirsp = 15.0f;
    int jumpcount = 0;
    public static bool isPlaying = false;
    int branchcount = 0;
    public GameObject BranchL;
    public GameObject BranchR;
    static bool jump = false;
    public int pass;
    Rigidbody rb;
    Animator animator;
    public GameObject Camera;
    public GameObject card;
    MainManegerScript MS;
    public GameObject MainManager;
    Vector3 Gpos;
    float Grot;
    NavMeshAgent agent;

    AudioSource Audio;
    public AudioClip GhostSE;
    public AudioClip JumpSE;
    public AudioClip DeadSE1;
    public AudioClip DeadSE2;
    public AudioClip UpSE;
    public AudioClip DownSE;
    public AudioClip BananaSE;
    public AudioClip CardSE;
    //public AudioClip AshiSE;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        MS = MainManager.GetComponent<MainManegerScript>();
        speed = spfirst;
        animator = this.GetComponent<Animator>();
        Audio = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        BranchL.SetActive(false);
        BranchR.SetActive(false);
        Audio.Play();

        if (isPlaying == false)
        {
            PlayerPrefs.SetFloat("Gposx", 0);
            PlayerPrefs.SetFloat("Gposy", 0);
            PlayerPrefs.SetFloat("Gposz", 0);
            PlayerPrefs.SetFloat("Grot", 0);
            Debug.Log("Start F");
            pass = 1;
        }
        else
        {
            Gpos.x = PlayerPrefs.GetFloat("Gposx");
            Gpos.y = PlayerPrefs.GetFloat("Gposy");
            Gpos.z = PlayerPrefs.GetFloat("Gposz");
            Grot = PlayerPrefs.GetFloat("Grot");
            pass = PlayerPrefs.GetInt("PASS");
            agent.enabled = false;
            this.transform.position = Gpos;
            this.transform.eulerAngles = new Vector3(0, Grot, 0);
            agent.enabled = true;
            Debug.Log("Start T");
        }

        Debug.Log("Gpos" + Gpos);
        Debug.Log("this.transform.position" + this.transform.position);
        Debug.Log("Grot" + Grot);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == true)
        {

            this.transform.position += this.transform.forward * speed * Time.deltaTime;

            var dir = Vector3.zero;
            dir.x += Input.acceleration.x;
            dir.z -= Input.acceleration.y;

            // clamp acceleration vector to the unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            dir *= Time.deltaTime;
            transform.Translate(dir * dirsp);

            FlickUp();
        }

        if (jump == true)
        {
            this.transform.position += new Vector3(0, jumpPower, 0);
        }

        Pass();

        if (Input.GetKey("left"))
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("right"))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

    }

    void GetDirection()
    {
        float dirX = touchEndPos.x - touchStartPos.x;
        float dirY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(dirY) < Mathf.Abs(dirX))
        {
            if (30 < dirX)
            {
                direction = "right";
            }
            else if (dirX < -30)
            {
                direction = "left";
            }
        }
        else if (Mathf.Abs(dirY) > Mathf.Abs(dirX))
        {
            if (30 < dirY)
            {
                direction = "up";
            }
            else if (dirY < -30)
            {
                direction = "down";
            }
        }
        else
        {
            direction = "touch";
        }

    }

    void FlickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            GetDirection();

            if (direction == "up" && jumpcount < 1)
            {
                Debug.Log("up");
                Audio.Stop();
                jump = true;
                jumpcount++;
                Camera.transform.position -= new Vector3(0, jumpPower, 0);
                Invoke("Jumping", 1.55f);

                animator.SetBool("Jump", true);
                Audio.PlayOneShot(JumpSE);

            }
        }
    }
    void Flick2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            GetDirection();

            switch (direction)
            {
                case "right":
                    this.transform.Rotate(0, 90, 0, Space.Self);
                    Debug.Log("R");
                    branchcount++;
                    break;

                case "left":
                    this.transform.Rotate(0, -90, 0, Space.Self);
                    Debug.Log("L");
                    branchcount++;
                    break;
            }
        }
    }

    void Jumping()
    {
        jump = false;
        Camera.transform.position += new Vector3(0, jumpPower, 0);
        Audio.Play();
    }

    void Pass()
    {
        if (pass == 1)
        {
            card.SetActive(true);
        }
        else
        {
            card.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "protein")
        {
            Debug.Log("protein");
            Audio.PlayOneShot(UpSE);
            StartCoroutine("SpeedUp");
            Destroy(other.gameObject);
            Audio.Play();
        }

        if (other.gameObject.tag == "beer")
        {
            Audio.Stop();
            Debug.Log("beer");
            Audio.PlayOneShot(DownSE);
            StartCoroutine("SpeedDown");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "banana")
        {
            Audio.Stop();
            animator.SetTrigger("FallDown");
            Invoke("bananaSE", 1);
            speed = spfirst;
            Destroy(other.gameObject);
            pass = 0;
        }

        if (other.gameObject.tag == "ghost")
        {
            Audio.Stop();
            PlayerPrefs.SetFloat("TIMER", MS.timer);
            PlayerPrefs.SetInt("PASS", pass);
            Gpos.x = this.transform.position.x;
            Gpos.y = this.transform.position.y;
            Gpos.z = this.transform.position.z;
            Grot = this.transform.eulerAngles.y;
            PlayerPrefs.SetFloat("Gposx", Gpos.x);
            PlayerPrefs.SetFloat("Gposy", Gpos.y);
            PlayerPrefs.SetFloat("Gposz", Gpos.z);
            PlayerPrefs.SetFloat("Grot", Grot);
            Destroy(other.gameObject, 1.5f);
            StartCoroutine("MvGhost");
            Debug.Log(Grot);
        }

        if (other.gameObject.tag == "deadend")
        {
            Audio.Stop();
            isPlaying = false;
            StartCoroutine("DeadEnd");
            Invoke("GameOver", 3);
        }

        if (other.gameObject.tag == "road")
        {
            jumpcount = 0;
        }

        if (other.gameObject.tag == "ic")
        {
            Audio.Stop();
            pass = 1;
            Audio.PlayOneShot(CardSE);
            Destroy(other.gameObject);
            Audio.Play();
        }

        if (other.gameObject.tag == "wall")
        {
            Debug.Log("“–‚½‚Á‚½");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "branch" && branchcount < 1)
        {
            Flick2();
            BranchL.SetActive(true);
            BranchR.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "branch")
        {
            BranchL.SetActive(false);
            BranchR.SetActive(false);
            branchcount = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "goal")
        {
            BranchL.SetActive(false);
            BranchR.SetActive(false);
            branchcount = 0;
            Goal();
        }
    }

    IEnumerator SpeedUp()
    {
        if (speed < 35)
        {
            speed += 10.0f;
        }else{
            speed = 35;
        }

            yield return new WaitForSeconds(3.0f);
            speed = spfirst;
        }

        IEnumerator SpeedDown()
        {
            animator.SetBool("Drink", true);
            speed = 5.0f;
            yield return new WaitForSeconds(3.0f);
            speed = spfirst;
            animator.SetBool("Drink", false);
            Audio.Play();
    }

        void GameOver()
        {
            isPlaying = false;
            SceneManager.LoadScene("GameOverScene");
            Debug.Log(isPlaying);
        }

        void Goal()
        {
            isPlaying = false;
            PlayerPrefs.SetFloat("MainTimer", MS.timer);
            PlayerPrefs.SetInt("PASS", pass);
            SceneManager.LoadScene("StationScene");
        }

        void bananaSE()
        {
            Audio.PlayOneShot(BananaSE);
            Audio.Play();
    }

        IEnumerator MvGhost()
        {
            isPlaying = false;
            Audio.PlayOneShot(GhostSE);
            yield return new WaitForSeconds(2);
            isPlaying = true;
            SceneManager.LoadScene("GhostScene");
        }

        IEnumerator DeadEnd()
        {
            Audio.PlayOneShot(DeadSE1);
            animator.SetTrigger("blow");
            this.transform.position += -this.transform.forward * 5;
            yield return new WaitForSeconds(1f);
            Audio.PlayOneShot(DeadSE2);
        }
    }


