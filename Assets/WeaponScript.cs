﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laser;
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
        energy = GameController.Instance.getEnergy();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        GameObject lasershot = GameObject.FindGameObjectWithTag("Laser");
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
            
        }
    }
    void Shoot()
    {
        GameController.Instance.useEnergy(damage);
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 30);
        StartLaserAnim();
        GameObject clone = (GameObject)Instantiate(laser, new Vector3(firePoint.position.x + 5, firePoint.position.y - 0.57f, firePoint.position.z), Quaternion.identity, player.transform);
        Destroy(clone, 1.0f);
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
