using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateScript : MonoBehaviour
{
    public float health;
    public float baseDamage;

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health < 0) { Death(); }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag == "PlayerSword" && other.GetComponent<SwordScript>().getRes)
        {
            Damage(other.GetComponent<SwordScript>().resultDamage);
        }
        */
    }
}
