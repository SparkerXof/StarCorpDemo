using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public float baseSpeed;
    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CombatScript>().Damage();
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * baseSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }
}
