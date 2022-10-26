using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject beer;
    public GameObject protein;
    public GameObject banana;
    public GameObject ghost;
    public GameObject car;
    public GameObject ic;
    GameObject beerClone;
    GameObject proteinClone;
	GameObject bananaClone;
    GameObject ghostClone;
    GameObject carClone;
    GameObject icClone;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        int i;
        for (i = 0; i <= 200; i++)
        {
            Protein();
        }

        for (i = 0; i <= 100; i++)
        {
            Banana();
            Beer();
        }

        for (i = 0; i <= 10; i++)
        {
            Ghost();
            Car();
            Ic();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Beer()
    {
        beerClone = Instantiate(beer, new Vector3(Random.Range(-930f, -30f), 6, Random.Range(-520f, -50f)),this.transform.rotation) as GameObject;
    }
    void Protein()
    {
        proteinClone = Instantiate(protein, new Vector3(Random.Range(-930f, -30f), 6, Random.Range(-520f, -50f)), this.transform.rotation) as GameObject;
    }

    void Banana()
    {
        bananaClone = Instantiate(banana, new Vector3(Random.Range(-930f, -30f), 6, Random.Range(-520f, -50f)), this.transform.rotation) as GameObject;
    }
    void Ghost()
    {
        ghostClone = Instantiate(ghost, new Vector3(Random.Range(-700f, -200f), 10, Random.Range(-520f, -50f)), this.transform.rotation) as GameObject;
    }
    void Car()
    {
        carClone = Instantiate(car, new Vector3(Random.Range(-800f, -30f), 10, Random.Range(-520f, -100f)), this.transform.rotation) as GameObject;
    }
    void Ic()
    {
        icClone = Instantiate(ic, new Vector3(Random.Range(-930f, -500f), 15, Random.Range(-550f, -90f)), this.transform.rotation) as GameObject;
    }

}
