using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Animator anim;
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    private bool isSleep = true;
    public float WakeupRadiusFromPlayer = 7;

    public Vector3 lastTransform;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
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
}
