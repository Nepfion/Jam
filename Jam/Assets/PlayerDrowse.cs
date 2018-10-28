using UnityEngine;
using UnityEngine.UI;

public class PlayerDrowse : MonoBehaviour {

    public PlayerController PlayerController;
    public float StartingDrowse = 100f;
    public float CurrentDrowse;
    public float DrowseRate = 15f;
    public Text DrowseText;
    public Text FinalText;
    public Image DrowseImage;
    public Color SleepColor = new Color(1, 1, 1);
    //public AudioClip SleepClip;

    public bool Sleeping = false;

    public AudioControl AudioControl;
    private AudioSource audioSource;
    public AudioClip Drowse;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        CurrentDrowse = StartingDrowse;
    }

    private void Update()
    {
        if (Sleeping || GetComponent<PlayerHealth>().IsDead) return;

        CurrentDrowse -= DrowseRate * Time.deltaTime;
        if (CurrentDrowse < 0.0f)
        {
            CurrentDrowse = 0.0f;
            Sleeping = true;
            anim.SetTrigger("Sleep");
            FinalText.text = "You fell asleep!";
            FinalText.color = Color.yellow;
            audioSource.clip = Drowse;
            audioSource.Play();
        }



        if (CurrentDrowse > StartingDrowse)
        {
            CurrentDrowse = StartingDrowse;
        }
        
        PlayerController.Speed = PlayerController.InitialSpeed * CurrentDrowse / StartingDrowse;

        DrowseText.text = ((int)CurrentDrowse).ToString() + " / " + StartingDrowse.ToString();
        DrowseImage.color = Color.Lerp(SleepColor, Color.clear, CurrentDrowse / StartingDrowse);
        AudioControl.BlendSnapshots(CurrentDrowse, StartingDrowse);

        anim.SetFloat("Sleeping", CurrentDrowse / StartingDrowse);

    }

}
