using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class Win : MonoBehaviour {
    public Text FinalText;
    public GameManager GameManager;
    public void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        PlayerShooting playerShooting = other.gameObject.GetComponentInChildren<PlayerShooting>();

        if (playerController != null)
        {
            FinalText.color = Color.green;
            FinalText.text = "You won!";
            playerController.enabled = false;
            playerShooting.enabled = false;
            GameManager.IsWin = true;
        }
    }
}
