using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemScript : MonoBehaviour
{
    GameObject itemClone;
    NavMeshAgent agent;
    NavMeshHit hit;
    float mvx;
    float mvz;
    Vector3 FirstPos;
    Vector3 MovePos;
    public int Rotx;
    public int Roty;
    public int Rotz;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FirstPos = this.transform.position;

        if (NavMesh.SamplePosition(this.transform.position, out hit, 100f, NavMesh.AllAreas))
        {
            this.transform.position = hit.position;
        }

        Destroy(agent);
        this.transform.Rotate(Rotx, Roty, Rotz); ;

        MovePos = this.transform.position;

        mvx = FirstPos.x - MovePos.x;
        mvz = FirstPos.z - MovePos.z;

        if (Mathf.Abs(mvx) >= Mathf.Abs(mvz))
        {
            if (mvx >= 0)
            {
                this.transform.position -= new Vector3(Random.Range(0, 7), 0, 0);
            }else{
                this.transform.position += new Vector3(Random.Range(0, 7), 0, 0);
            }
        }
        else if (Mathf.Abs(mvx) < Mathf.Abs(mvz))
        {
            if (mvx >= 0)
            {
                this.transform.position -= new Vector3(0, 0, Random.Range(0, 7));
            }else {
                this.transform.position += new Vector3(0, 0, Random.Range(0, 7));
            }

        }


        }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "destory")
        {
            Destroy(this.gameObject);
            Debug.Log("des");
        }
    }
}
