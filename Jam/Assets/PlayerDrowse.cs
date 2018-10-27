using UnityEngine;
using UnityEngine.UI;

public class PlayerDrowse : MonoBehaviour {

    public PlayerController PlayerController;
    public float StartingDrowse = 100f;
    public float CurrentDrowse;
    public float DrowseRate = 15f;
    public Text DrowseText;
    //public Image SleepImage;
    //public AudioClip SleepClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 1f, 0f, 0.1f);

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        CurrentDrowse = StartingDrowse;
    }

    private void Update()
    {
        CurrentDrowse -= DrowseRate * Time.deltaTime;
        if (CurrentDrowse < 0.0f)
            CurrentDrowse = 0.0f;

        if (CurrentDrowse > StartingDrowse)
        {
            CurrentDrowse = StartingDrowse;
        }

        PlayerController.Speed = PlayerController.InitialSpeed * CurrentDrowse / StartingDrowse;

        DrowseText.text = ((int)CurrentDrowse).ToString() + " / " + StartingDrowse.ToString();
    }

}
