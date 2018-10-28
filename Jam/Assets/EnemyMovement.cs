using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    GameManager GameManager;
    NavMeshAgent nav;
    
    private bool isSleep = true;
    public bool isRunning = false;
    public float RunningTime = 5;
    public float WakeupRadiusFromPlayer = 7;

    public Vector3 lastTransform;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        lastTransform = transform.position;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Debug.Log(anim);
        anim.SetFloat("Sleeping", 1);
    }

    private void Update()
    {
        if (GameManager.IsWin)
            return;
        if (enemyHealth.CurrentHealth <= 0 && isRunning)
        {
            StopCoroutine(cor);
            nav.SetDestination(transform.position);
        }

        if (isRunning)
            return;

        if (isSleep)
        {
            if (Vector3.Distance(player.position, transform.position) < WakeupRadiusFromPlayer)
            {
                isSleep = false;
            }

        }
        if (!isSleep && nav.isOnNavMesh && !GameManager.IsWin)
        {
            if (enemyHealth.CurrentHealth > 0 && playerHealth.CurrentHealth > 0)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("Forward", (transform.position - lastTransform).normalized.z);
                anim.SetFloat("Strafe", (transform.position - lastTransform).normalized.x);
                nav.SetDestination(player.position);
                lastTransform = transform.position;
            }
            else
            {
                anim.SetBool("isWalking", false);
                nav.enabled = false;
            }
        }
    }

    public void Wakeup()
    {
        isSleep = false;
    }

    private Coroutine cor;

    public void Slap()
    {
        isRunning = true;
        cor = StartCoroutine(Run());
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
        cor = null;
    }
}
