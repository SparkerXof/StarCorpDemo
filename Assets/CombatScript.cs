using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    // Health
    public int health;
    public bool isDead = false;

    // Projectile attack
    public GameObject projectile;
    public Transform aimCamera;
    public Transform spawner;
    public float projectileSpeed;
    public float projectileDamage;
    public float shootDelay;
    private float shootDelayTimer;

    // Melee attack
    public bool canSlay;

    Animator animator;

    private void Start()
    {
        canSlay = true;
        animator = GetComponent<Animator>();
    }

    void Death()
    {
        isDead = true;
        GetComponent<PlayerMovementScript>().TurnDead();
    }

    public void Damage()
    {
        health -= 1;
        if (health < 0)
        {
            Death();
        }
    }

    void Update()
    {
        if (!isDead)
        {
            animator.ResetTrigger("Attack");
            if (Input.GetButton("Shoot"))
            {
                if (shootDelayTimer < 0)
                {
                    projectile.GetComponent<PlayerProjectileScript>().baseSpeed = projectileSpeed;
                    projectile.GetComponent<PlayerProjectileScript>().damage = projectileDamage;
                    Instantiate(projectile, spawner.position, aimCamera.rotation);
                    spawner.GetComponent<AudioSource>().Play();
                    shootDelayTimer = shootDelay;
                }
                shootDelayTimer -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Slay"))
            {
                if (canSlay)
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
}
