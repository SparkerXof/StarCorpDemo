using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public Transform rotatingPart;
    public Transform bulletSpawner;
    public GameObject projectile;
    public float projectileSpeed;
    private Transform player;

    public float shootDelay;
    private float shootDelayTimer;

    AudioSource audio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        rotatingPart.LookAt(player);
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawner.position, bulletSpawner.forward, out hit, 40f)) {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (shootDelayTimer < 0)
                {
                    projectile.GetComponent<EnemyProjectileScript>().baseSpeed = projectileSpeed;
                    Instantiate(projectile, bulletSpawner.position, rotatingPart.rotation);
                    audio.Play();
                    shootDelayTimer = shootDelay;
                }
                shootDelayTimer -= Time.deltaTime;
            }
        }
    }
}
