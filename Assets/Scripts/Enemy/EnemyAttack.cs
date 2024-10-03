using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyStats stats;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerStay (Collider other)
    {
        if(other.gameObject == player)
        {
            if (timer >= stats.timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {
                Attack();
            }
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;



        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger (stats.id_playerDead);
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (stats.attackDamage);
        }
    }
}
