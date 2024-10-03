using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    //public int startingHealth = 100;
    public int currentHealth;
    //public float sinkSpeed = 2.5f;
    //public int scoreValue = 10;
    //public AudioClip deathClip;

    [SerializeField] EnemyStats stats;

    Animator anim;
    
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    NavMeshAgent agent;
    Rigidbody rb;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        agent = GetComponent <NavMeshAgent> ();
        rb = GetComponent <Rigidbody> ();

        currentHealth = stats.startingHealth;
    }

    private void OnEnable()
    {
        isDead = false;
        isSinking = false;
        capsuleCollider.isTrigger = false;
        enemyAudio.clip = stats.hurtClip;
    }

    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * stats.sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger (stats.id_dead);

        enemyAudio.clip = stats.deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        agent.enabled = false;
        rb.isKinematic = true;
        isSinking = true;
        ScoreManager.score += stats.scoreValue;
        Destroy (gameObject, 2f);
    }
}
