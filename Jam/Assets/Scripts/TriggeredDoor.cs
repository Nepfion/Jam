using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState { Opened, Closed, Opening, Closing };
public class TriggeredDoor : MonoBehaviour {

    public TriggerSource[] TriggerSources;
    public int signalCount;

    public event Action OnTrigger;

    public DoorState DoorState;

    [SerializeField]
    private float doorOpeningDuration;
    private Vector3 openedPosition;
    private Vector3 closedPosition;

    Coroutine currentCoroutine;
    

    // Use this for initialization
    void Start () {
        foreach (TriggerSource src in TriggerSources)
        {
            src.OpenSignal += OpenSignal;
            src.CloseSignal += CloseSignal;
        }
        DoorState = DoorState.Opened;

        signalCount = 0;
        openedPosition = transform.position;

        BoxCollider collider = GetComponent<BoxCollider>();
        closedPosition = collider.bounds.center;
        closedPosition.y += 2 * (collider.bounds.max.y - closedPosition.y);
        
        currentCoroutine = StartCoroutine(lerpCloseDoor());
	}
	
	// Update is called once per frame
	void Update () {
	}


    private void OpenSignal()
    {
        signalCount++;
        //if (signalCount == TriggerSources.Length)
        //{
            StopCoroutine(currentCoroutine);

            if (DoorState != DoorState.Opening)
                currentCoroutine = StartCoroutine(lerpOpenDoor());
        //}
    }

    private void CloseSignal()
    {
        signalCount--;
        StopCoroutine(currentCoroutine);
        DoorState = DoorState.Opened;
        currentCoroutine = StartCoroutine(lerpCloseDoor());
    }

    private IEnumerator lerpOpenDoor()
    {
        float currentTime = 1 - (transform.position.y - openedPosition.y) / (closedPosition.y - openedPosition.y);
        DoorState = DoorState.Opening;

        Vector3 startPos = closedPosition;
        Vector3 endPos = openedPosition;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / doorOpeningDuration;
            transform.position = Vector3.Lerp(startPos, endPos, currentTime);
            yield return null;
        }

        DoorState = DoorState.Opened;
    }

    private IEnumerator lerpCloseDoor()
    {
        float currentTime = (transform.position.y - openedPosition.y) / (closedPosition.y - openedPosition.y);
        DoorState = DoorState.Closing;

        Vector3 startPos = openedPosition;
        Vector3 endPos = closedPosition;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / doorOpeningDuration;
            transform.position = Vector3.Lerp(startPos, endPos, currentTime);
            yield return null;
        }

        DoorState = DoorState.Closed;
    }
}
