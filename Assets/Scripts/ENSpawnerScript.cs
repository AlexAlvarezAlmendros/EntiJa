using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENSpawnerScript : MonoBehaviour
{
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;

    private GameObject Camara;

    public float maxDelay;
    public float minDelay;

    public float FirstSpawn = 0f;
    private float nextSpawn = 0f;

    public int whatToSpawn;
    public float whereToSpawn;
    public float whenToSpawn;

    void Start()
    {
        nextSpawn = FirstSpawn;
        Camara = GameObject.FindWithTag("MainCamera");
        Debug.LogError("ENS START");
    }

    void Update()
    {
        Debug.LogError("ENS READY");
        if (Time.time > nextSpawn) // if time has come
        {
            whatToSpawn = Random.Range(1, 3); //define random value between 1 and 4 (5 is ecluded)

            //whereToSpawn = Random.Range(0, 5); // define random value between 0 and 4 (5 is excluded)

            whenToSpawn = Random.Range(minDelay, maxDelay); // define random time value to spawn

            if (whatToSpawn == 1)
            {
                GameObject enemySpawned = Instantiate(Enemy1Prefab, new Vector3(Camara.transform.position.x + 10f, Camara.transform.position.y - 2f, 1f), Quaternion.identity);
            }
            else if (whatToSpawn == 2)
            {
                GameObject enemySpawned = Instantiate(Enemy2Prefab, new Vector3(Camara.transform.position.x + 10f, Camara.transform.position.y + 2f, 1f), Quaternion.identity);            
            }
            nextSpawn = Time.time + whenToSpawn;
        }
    }
}
