using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyHealth currentHP;
    PlayerHealth currentPlayer;
    NavMeshAgent agent;

    private void Awake()
    {
        currentHP = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPlayer = player.gameObject.GetComponent<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (currentHP.currentHealth > 0 && currentPlayer.currentHealth > 0)
        {
            agent.SetDestination (player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
