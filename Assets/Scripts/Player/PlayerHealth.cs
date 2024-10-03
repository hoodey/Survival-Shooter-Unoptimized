using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    int id_die = Animator.StringToHash("Die");

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    //bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount)
    {
        //damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }

        StartCoroutine(ScreenFlash());
    }

    IEnumerator ScreenFlash()
    {
        damageImage.color = flashColour;
        for (int i = 1; i <= 4; i++)
        {
            Debug.Log(i);
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, i/flashSpeed);
            yield return new WaitForSeconds(0.25f);
        }

    }

    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger (id_die);

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
