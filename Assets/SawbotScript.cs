using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SawbotScript : MonoBehaviour
{
    public Transform origin;
    private Transform player;
    private NavMeshAgent agent;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CombatScript>().Damage();
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, player.position - origin.position, out hit, 60f))
        {
            if (hit.collider.tag == "Player")
            {
                agent.SetDestination(player.position);
            }
        }
    }
}
