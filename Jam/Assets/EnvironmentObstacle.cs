using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObstacle : MonoBehaviour {
    public int DamageAmount;
    public int Duration;
    private float timer;
    public bool isDealingDamage;
    private MeshRenderer meshRenderer;
    private ParticleSystem particleSystem;

    private void Start()
    {
        timer = Duration;
        isDealingDamage = true;
        meshRenderer = GetComponent<MeshRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            isDealingDamage = !isDealingDamage;
            if (isDealingDamage)
                particleSystem.Play();
            else
                particleSystem.Stop();
            //Color color = meshRenderer.material.color;
            //color.a = isDealingDamage ? 1 : 0;
            //meshRenderer.material.color = color;
            timer = Duration;
        }
    }
    

    public void OnTriggerEnter(Collider other)
    {
        if (!isDealingDamage)
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(DamageAmount);
        }
    }
}
