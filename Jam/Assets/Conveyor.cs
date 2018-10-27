using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {
    public float Speed;
    private int maxDim;
    private Vector3 endPoint;
    private Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<Collider>().bounds;
        maxDim = 0;
        if (bounds.size[1] > bounds.size[0])
        {
            maxDim = 1;
        } else if (bounds.size[2] > bounds.size[1])
        {
            maxDim = 2;
        }

    }

    private void updateEndPoint(Vector3 targetPos)
    {
        endPoint = targetPos;
        endPoint[maxDim] = bounds.max[maxDim];
    }

    public void OnTriggerStay(Collider other)
    {
        updateEndPoint(other.transform.position);
        other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint, Speed * Time.deltaTime);
    }
}
