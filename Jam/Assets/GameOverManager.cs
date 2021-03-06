﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerHealth.CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        anim.SetTrigger("GameOver");

        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
