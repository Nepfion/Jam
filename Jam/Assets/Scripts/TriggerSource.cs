using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSource : MonoBehaviour {
    public event Action OpenSignal;
    public event Action CloseSignal;

    private void OnTriggerEnter()
    {
        if (OpenSignal != null)
            OpenSignal();
    }

    private void OnTriggerExit()
    {
        if (CloseSignal != null)
            CloseSignal();
    }
}
