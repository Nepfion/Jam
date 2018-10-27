using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int DamageAmount;
    public float Speed;
    private Rigidbody bulletRigidbody;
	// Use this for initialization
	void Start () {
        bulletRigidbody = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * Speed * Time.fixedDeltaTime;
        bulletRigidbody.MovePosition(transform.position + movement);
    }
    
    public void OnTriggerEnter(Collider collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(DamageAmount, transform.position);
        }

        Destroy(gameObject);
    }
}
