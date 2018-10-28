using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (PlayerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }


}
