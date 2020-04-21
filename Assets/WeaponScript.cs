using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public CarController car;
    public Transform firePoint;
    public GameObject laser;
    public GameObject exlosion;
    private GameObject player;
    private GameObject enemy;
    private Animator laserAnimator;
    private bool canShoot = true;
    public int shootDelay;
    private float energy;
    private int damage = 20;
    private int LaserShotID;

    void Start()
    {
        laserAnimator = laser.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        energy = GameController.instance.energy;
        if (Input.GetButtonDown("Fire1") && energy > 20 )
        {
            Shoot();
        }
    }
    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Disparo");
        if (car.boosted == false)
        {
            car.useEnergy(damage);
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 30);
        StartLaserAnim();
        GameObject clone = (GameObject)Instantiate(laser, new Vector3(firePoint.position.x + 5, firePoint.position.y - 0.57f, firePoint.position.z), Quaternion.identity, player.transform);
        Destroy(clone, 1.0f);
        if (hitInfo)
        {
            Enemy2Script enemy = hitInfo.transform.GetComponent<Enemy2Script>();
            Enemy1Script enemy2 = hitInfo.transform.GetComponent<Enemy1Script>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
            }else if (enemy2 != null )
            {
                enemy2.takeDamage(damage);
            }
            GameObject cloneexplosion = (GameObject)Instantiate(exlosion, hitInfo.point, Quaternion.identity);
            Destroy(cloneexplosion, 1.0f);

        }

    }
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSecondsRealtime(2);
        canShoot = true;
    }
    public void StartLaserAnim()
    {
        laserAnimator.SetTrigger("LaserShot");
        //canShoot = false;
    }
}
