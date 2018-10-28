using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int StartingHealth = 2;
    public int CurrentHealth;
    public int ScoreValue = 10;
    public AudioClip DeathClip;

    Animator anim;
    //AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        //enemyAudio.Play();

        CurrentHealth -= amount;

        //hitParticles.transform.position = hitPoint;

        //hitParticles.Play();

        if (CurrentHealth <= 0)
        {
            Death();
            return;
        }
        anim.SetTrigger("Hit");
    }

    void Death()
    {
        isDead = true;

        //capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        //GetComponent<NavMeshAgent>().enabled = false;

        //enemyAudio.clip = DeathClip;
        //enemyAudio.Play();

    }

}
