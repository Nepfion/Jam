using UnityEngine;
using UnityEngine.UI;

public class PlayerDrowse : MonoBehaviour {

    public PlayerController PlayerController;
    public float StartingDrowse = 100f;
    public float CurrentDrowse;
    public float DrowseRate = 15f;
    public Text DrowseText;
    public Image DrowseImage;
    //public AudioClip SleepClip;

    public bool Sleeping = false;

    public AudioControl AudioControl;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        CurrentDrowse = StartingDrowse;
    }

    private void Update()
    {
        if (Sleeping) return;

        CurrentDrowse -= DrowseRate * Time.deltaTime;
        if (CurrentDrowse < 0.0f)
        {
            CurrentDrowse = 0.0f;
            Sleeping = true;
            anim.SetTrigger("Sleep");
        }



        if (CurrentDrowse > StartingDrowse)
        {
            CurrentDrowse = StartingDrowse;
        }
        
        PlayerController.Speed = PlayerController.InitialSpeed * CurrentDrowse / StartingDrowse;

        DrowseText.text = ((int)CurrentDrowse).ToString() + " / " + StartingDrowse.ToString();
        DrowseImage.color = Color.Lerp(Color.white, Color.clear, CurrentDrowse / StartingDrowse);
        AudioControl.BlendSnapshots(CurrentDrowse, StartingDrowse);

        anim.SetFloat("Sleeping", CurrentDrowse / StartingDrowse);

    }

}
