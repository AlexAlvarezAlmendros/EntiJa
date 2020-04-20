using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENSpawnerScript : MonoBehaviour
{
    public GameObject Enemy1Prefab;
    private Enemy1Script enemy1Script;

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
    }

    void Update()
    {
        if (Time.time > nextSpawn) // if time has come
        {
            whatToSpawn = Random.Range(1, 2); //define random value between 1 and 4 (5 is ecluded)

            //whereToSpawn = Random.Range(0, 5); // define random value between 0 and 4 (5 is excluded)

            whenToSpawn = Random.Range(minDelay, maxDelay); // define random time value to spawn

            if (whatToSpawn == 1)
            {
                GameObject enemySpawned = Instantiate(Enemy1Prefab, new Vector3(Camara.transform.position.x + 10f, Camara.transform.position.y - 2f, 1f), Quaternion.identity);
                enemy1Script = enemySpawned.GetComponent<Enemy1Script>();
                //enemyScript.powerUpType = powerUpType.Energy;
            }
            //else if (whatToSpawn == 2)
            //{
            //    GameObject enemySpawned = Instantiate(Enemy2Prefab, new Vector3(-5.5f, whereToSpawn, 1f), Quaternion.identity);
            //    powerUpScript = enemySpawned.GetComponent<PowerUpScript>();
            //    powerUpScript.powerUpType = powerUpType.Boost;
            //}
            //else if (whatToSpawn == 3)
            //{
            //    GameObject enemySpawned = Instantiate(PowerUpPrefab, new Vector3(-5.5f, whereToSpawn, 1f), Quaternion.identity);
            //    powerUpScript = enemySpawned.GetComponent<PowerUpScript>();
            //    powerUpScript.powerUpType = powerUpType.Shield;
            //}
            //else if (whatToSpawn == 4)
            //{
            //    GameObject enemySpawned = Instantiate(PowerUpPrefab, new Vector3(-5.5f, whereToSpawn, 1f), Quaternion.identity);
            //    powerUpScript = enemySpawned.GetComponent<PowerUpScript>();
            //    //powerUpScript.powerUpType = powerUpType.PW4;
            //}

            nextSpawn = Time.time + whenToSpawn;
        }
    }
}
