using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class Win : MonoBehaviour {
    public Text winText;

    public void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            Color color = winText.color;
            color.a = 1;
            winText.color = color;
        }
    }
}
