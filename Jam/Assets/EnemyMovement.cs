using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    private bool isSleep = true;
    public float WakeupRadiusFromPlayer = 7;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isSleep)
        {
            if (Vector3.Distance(player.position, transform.position) < WakeupRadiusFromPlayer)
            {
                isSleep = false;
            }

        }
        if (!isSleep && nav.isOnNavMesh)
        {
            if (enemyHealth.CurrentHealth > 0 && playerHealth.CurrentHealth > 0)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }

    public void Wakeup()
    {
        isSleep = false;
    }
}
