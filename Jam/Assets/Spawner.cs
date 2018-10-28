using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    private bool isActive;
    public bool IsActive {
        get {
            return isActive;
        }
        set
        {
            isActive = value;
            if (value)
                InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
    }
    public int SpawnCounter = 1;
    private EnemyHealth currentEnemy;
    public GameObject enemy;

    public float spawnTime = 3f;


    private void Start()
    {
        currentEnemy = null;
    }

    void Update () {
	}

    private void Spawn()
    {
        if (currentEnemy == null || (currentEnemy.CurrentHealth <= 0 && SpawnCounter > 0)) {
            currentEnemy = Instantiate(enemy, transform.position, transform.rotation).GetComponent<EnemyHealth>();
            SpawnCounter--;
        }        
    }
}
