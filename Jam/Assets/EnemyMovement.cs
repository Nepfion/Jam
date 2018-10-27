using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    
    private bool isSleep = true;
    private bool isRunning = false;
    public float RunningTime = 5;
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
        if (isRunning)
            return;

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

    public void Slap()
    {
        isRunning = true;
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 20, 1);
        nav.destination = hit.position;
        
        yield return new WaitForSeconds(RunningTime);

        isRunning = false;
    }
}
