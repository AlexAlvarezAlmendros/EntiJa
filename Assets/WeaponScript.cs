using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laser;
    public GameObject enemy;
    public Animator laserAnimator;
    private bool canShoot = true;
    public int shootDelay;
    private float energy;
    private int damage = 20;
    private int LaserShotID;

    void Start()
    {
        laserAnimator = laser.GetComponent<Animator>();
        energy = GameController.Instance.getEnergy();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 30);
        StartLaserAnim();

        if (hitInfo)
        {
            Enemy2Script enemy = hitInfo.transform.GetComponent<Enemy2Script>();
            if(enemy != null)
            {
                enemy.takeDamage(damage);
            }
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
        canShoot = false;
    }
}
