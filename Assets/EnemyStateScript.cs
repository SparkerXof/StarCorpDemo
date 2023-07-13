using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateScript : MonoBehaviour
{
    public float health;

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health < 0) { Death(); }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
