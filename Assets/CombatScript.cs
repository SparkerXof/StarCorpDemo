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
    public AudioSource swordAudio;
    public float projectileSpeed;
    public float projectileDamage;
    public float shootDelay;
    private float shootDelayTimer;
    public float damageDelay;
    private float damageDelayTimer;

    // Melee attack
    public bool canSlay;

    public GameObject shield;

    Animator animator;

    private void Start()
    {
        canSlay = true;
        animator = GetComponent<Animator>();
        FindAnyObjectByType<MainGameManager>().UpdateUI();
    }

    void Death()
    {
        isDead = true;
        FindAnyObjectByType<MainGameManager>().UpdateLabel("You lose!\nTry again");
        GetComponent<PlayerMovementScript>().TurnDead();
        animator.SetTrigger("Death");
    }

    public void Damage()
    {
        if (damageDelayTimer < 0)
        {
            health -= 1;
            if (health < 0)
            {
                Death();
            } 
            else
            {
                FindAnyObjectByType<MainGameManager>().UpdateUI();
                damageDelayTimer = damageDelay;
                shield.SetActive(true);
            }
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
                    swordAudio.Play();
                }
            }
        }
        damageDelayTimer -= Time.deltaTime;
        if (damageDelayTimer < 0)
        {
            shield.SetActive(false);
        }
    }
}
