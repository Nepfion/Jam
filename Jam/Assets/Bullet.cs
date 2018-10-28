using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int DamageAmount;
    public float Speed;
    private Rigidbody bulletRigidbody;
    public float bulletShotSoundRadius;
    public PlayerShooting playerShooting;
	// Use this for initialization
	void Start () {
        bulletRigidbody = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindWithTag("Player");
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * Speed * Time.fixedDeltaTime;
        bulletRigidbody.MovePosition(transform.position + movement);
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null && enemyHealth.CurrentHealth > 0)
        {
            enemyHealth.TakeDamage(DamageAmount, transform.position);
            playerShooting.updateBulletCount(1);
        }

        LayerMask mask = LayerMask.GetMask("Shootable");
        Collider[] colliders = Physics.OverlapSphere(transform.position, bulletShotSoundRadius);

        foreach (var collider in colliders)
        {
            EnemyMovement enemyMovement = collider.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.Wakeup();
            }
        }

        Destroy(gameObject);
    }
}
