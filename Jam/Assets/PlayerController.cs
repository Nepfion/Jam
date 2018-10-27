using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float InitialSpeed = 4;
    public float Speed = 4;

    public bool Aiming { get; private set; }
    public bool CanMove = true;

    private float jumpTime = 0;
    public bool isJumping;
    private Func<float, float> jumpFunc;
    private Vector3 startJumpPos, endJumpPos;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponentInChildren<Animator>();
        isJumping = false;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.Paused)
            return;

        playerRigidbody.velocity = Vector3.zero;
        if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime / 2;
            Vector3 pos = Vector3.Lerp(startJumpPos, endJumpPos, jumpTime);

            pos.y = jumpFunc(jumpTime);
            playerRigidbody.MovePosition(pos);

            if (jumpTime > 1)
                isJumping = false;
        }
        else if (CanMove && !GetComponent<PlayerHealth>().IsDead && !GetComponent<PlayerDrowse>().Sleeping)
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
        jumpTime = 0;
        isJumping = true;
        startJumpPos = startPos;
        endJumpPos = endPos;
        jumpFunc = movement;
        //StartCoroutine(lerpJump(startPos, endPos, movement));
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

        anim.SetFloat("Forward", -v * Speed);
        anim.SetFloat("Strafe", h);

    }
    
}
