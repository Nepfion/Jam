using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {

    public GameObject Target;

    private Func<float, float> jumpFunc = (float t) => 3 * (float)Math.Sin(t * Math.PI);

    public void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.isJumping)
            {
                playerController.isJumping = false;
            }
            else
            {
                playerController.Jump(transform.position, Target.transform.position, jumpFunc);
            }
        }
    }
}
