using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float baseDamage;
    public float resultDamage;
    public bool isSlaying;

    private void Start()
    {
        resultDamage = baseDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isSlaying)
        {
            other.gameObject.GetComponent<EnemyStateScript>().Damage(resultDamage);
        }
    }

    public void ChangeDamage(float delta)
    {
        resultDamage = baseDamage + delta;
    }
}
