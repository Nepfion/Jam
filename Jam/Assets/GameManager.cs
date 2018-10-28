using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public bool Paused = false;
    public bool IsWin = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {

    }
}
