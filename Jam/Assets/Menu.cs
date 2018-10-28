using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject play;
    public GameObject pause;

    public void Play()
    {
        Time.timeScale = 1;
        play.SetActive(false);
        pause.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        play.SetActive(true);
        pause.SetActive(false);
    }
}
