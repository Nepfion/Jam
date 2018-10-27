﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 2;
    public int CurrentHealth;
    public Text healthText;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerController playerController;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        CurrentHealth = StartingHealth;
    }
    /*
    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        } else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }
    */
    public void TakeDamage(int amount)
    {
        damaged = true;

        CurrentHealth -= amount;

        anim.SetTrigger("Hit");

        healthText.text = CurrentHealth.ToString();

        //playerAudio.Play();

        if(CurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();


        Debug.Log("You're dead!");
        anim.SetTrigger("Dead");

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerController.enabled = false;
        playerShooting.enabled = false;
    }


}
