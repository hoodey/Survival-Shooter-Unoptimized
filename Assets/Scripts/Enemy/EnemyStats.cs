using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public int startingHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public AudioClip hurtClip;

    public int id_playerDead = Animator.StringToHash("PlayerDead");
    public int id_dead = Animator.StringToHash("Dead");
}
