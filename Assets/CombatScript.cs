using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
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

    void Update()
    {
        animator.ResetTrigger("Attack");
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
        if (Input.GetButtonDown("Slay"))
        {
            if (canSlay)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
}
