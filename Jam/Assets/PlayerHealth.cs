using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead;

    public int StartingHealth = 2;
    public int CurrentHealth;
    public RectTransform healthPanel;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerController playerController;
    PlayerShooting playerShooting;
    public Text FinalText;
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
        if (GetComponent<PlayerDrowse>().Sleeping) return;

        damaged = true;

        CurrentHealth -= amount;
        if (healthPanel.childCount > 0)
            Destroy(healthPanel.GetChild(healthPanel.childCount - 1).gameObject);

        //playerAudio.Play();

        if(CurrentHealth <= 0 && !IsDead)
        {
            Death();
            return;
        }

        anim.SetTrigger("Hit");
    }

    void Death()
    {
        IsDead = true;

        playerShooting.DisableEffects();
        
        Debug.Log("You're dead!");
        anim.SetTrigger("Dead");
        FinalText.text = "You died!";
        FinalText.color = Color.red;

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerController.enabled = false;
        playerShooting.enabled = false;
    }


}
