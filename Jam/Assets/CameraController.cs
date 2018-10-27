using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, 100, LayerMask.GetMask("Floor")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;

                playerToMouse.y = 0f;

                targetCamPos += 4 * playerToMouse.normalized;
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }

}
