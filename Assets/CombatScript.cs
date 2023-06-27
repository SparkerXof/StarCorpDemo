using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform aimCamera;
    public Transform spawner;
    public float projectileSpeed;
    public float projectileDamage;
    public float shootDelay;
    private float shootDelayTimer;
    
    void Update()
    {
        if (Input.GetButton("Shoot"))
        {
            if (shootDelayTimer < 0)
            {
                projectile.GetComponent<PlayerProjectileScript>().baseSpeed = projectileSpeed;
                projectile.GetComponent<PlayerProjectileScript>().damage = projectileDamage;
                Instantiate(projectile, spawner.position, aimCamera.rotation);
                shootDelayTimer = shootDelay;
            }
            shootDelayTimer -= Time.deltaTime;
        }
    }
}
