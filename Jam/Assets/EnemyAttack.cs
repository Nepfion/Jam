using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float TimeBetweenAttacks = 0.5f;
    public int AttackDamage = 1;

    //Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerDrowse playerDrowse;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;
    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerDrowse = player.GetComponent<PlayerDrowse>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        //anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TimeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth > 0 && !enemyMovement.isRunning)
        {
            Attack();
        }

        if (playerHealth.CurrentHealth <= 0)
        {
            //anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.CurrentHealth > 0)
        {
            playerHealth.TakeDamage(AttackDamage);
            playerDrowse.CurrentDrowse = 100;
            enemyMovement.Slap();
        }
    }
}
