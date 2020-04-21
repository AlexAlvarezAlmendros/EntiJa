using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWSpawnerScript : MonoBehaviour
{
    public GameObject PowerUpPrefab;
    private PowerUpScript powerUpScript;

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
            whatToSpawn = Random.Range(1, 5); //define random value between 1 and 4 (5 is ecluded)

            whereToSpawn = Random.Range(Camara.transform.position.y - 2f, Camara.transform.position.y + 2f);

            whenToSpawn = Random.Range(minDelay, maxDelay); // define random time value to spawn

            if (whatToSpawn == 1)
            {
                GameObject powerSpawned = Instantiate(PowerUpPrefab, new Vector3(Camara.transform.position.x + 10f, whereToSpawn, 1f), Quaternion.identity);
                powerUpScript = powerSpawned.GetComponent<PowerUpScript>();
                powerUpScript.powerUpType = PowerUp.Energy;
            }
            else if (whatToSpawn == 2)
            {
                GameObject powerSpawned = Instantiate(PowerUpPrefab, new Vector3(Camara.transform.position.x + 10f, whereToSpawn, 1f), Quaternion.identity);
                powerUpScript = powerSpawned.GetComponent<PowerUpScript>();
                powerUpScript.powerUpType = PowerUp.Boost;
            }
            else if (whatToSpawn == 3)
            {
                GameObject powerSpawned = Instantiate(PowerUpPrefab, new Vector3(Camara.transform.position.x + 10f, whereToSpawn, 1f), Quaternion.identity);
                powerUpScript = powerSpawned.GetComponent<PowerUpScript>();
                powerUpScript.powerUpType = PowerUp.Shield;
            }

            nextSpawn = Time.time + whenToSpawn;
        }
    }
}
