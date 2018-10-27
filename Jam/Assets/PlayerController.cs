using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float InitialSpeed = 4;
    public float Speed = 4;

    public bool Aiming { get; private set; }
    public bool CanMove = true;
    public bool isJumping;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
<<<<<<< HEAD

        anim = GetComponentInChildren<Animator>();
=======
        isJumping = false;
        //anim = GetComponent<Animator>();
>>>>>>> 12e7e03c24ead40a46ddf03794e9249e50e38332
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.Paused)
            return;

        if (CanMove && !isJumping)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);

            Turning();

            Animating(h, v);
        }
        else
        {
            Animating(0, 0);
        }

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * Speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Jump(Vector3 startPos, Vector3 endPos, Func<float, float> movement)
    {
        StartCoroutine(lerpJump(startPos, endPos, movement));
    }

    private IEnumerator lerpJump(Vector3 startPos, Vector3 endPos, Func<float, float> movement)
    {
        float jumpTime = 2;
        isJumping = true;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / jumpTime;
            Vector3 pos = Vector3.Lerp(startPos, endPos, t);

            pos.y = movement(t);
            Debug.Log(pos.y);
            transform.position = pos;

            yield return null;
        }

        isJumping = false;
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;

        anim.SetBool("isWalking", walking);

        var dot = Vector3.Dot(transform.position.normalized, (Vector3.forward * v + Vector3.right * h).normalized);
        var val = Quaternion.LookRotation(transform.forward) * (Vector3.forward * v + Vector3.right * h);

        anim.SetFloat("Speed", val.z * Speed);
        anim.SetFloat("Turn", val.x);

    }
    
}
