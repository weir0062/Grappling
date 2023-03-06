using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Default,
        Pulling
    }
    public float MaxSpeed = 0.69f;
    public float Acceleration = 6.9f;
    public float Deceleration = 0.69f;
    public float MouseXSensitivity = 1.0f;
    public float MouseYSensitivity = 1.0f;
    float XRotation;
    float YRotation;
    public Transform orient;
    public Rigidbody body;
    public Camera bodycam;
    bool IsAccelerating = false;
    Vector3 endposition;
    public Gun gun;
    

    PlayerState state;
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Default;
        Cursor.lockState = CursorLockMode.Locked;
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayerState.Default:
                HandleMovement();
                HandleRotation();
                break;
            case PlayerState.Pulling:
                if((transform.position-endposition).sqrMagnitude < 1)
                {
                    SetState(PlayerState.Default);
                    gun.OnFinishedPulling();
                }
                Debug.Log("IsPulling");
                break;
            default:
                break;
        }
    }


    void HandleMovement()
    {

        IsAccelerating = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D);
        
            Vector3 velocity = body.velocity;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward * Acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward * Acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right * Acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.right * Acceleration * Time.deltaTime;
        }

        if(velocity.sqrMagnitude > MaxSpeed*MaxSpeed)
        {
            velocity = velocity.normalized * MaxSpeed;
        }

        if (!IsAccelerating && velocity!= Vector3.zero)
        {
            Vector3 poop =  velocity.normalized * Time.deltaTime * Deceleration;


            if(poop.magnitude> velocity.magnitude)
            {
                velocity = Vector3.zero;
                state = PlayerState.Default;
            }
            else
            {
                velocity -= poop;
            }
        }

        body.velocity = velocity;
        
    }
   
    public void PullTo(Vector3 endpos)
    {
        SetState(PlayerState.Pulling);
        Vector3 dir= endpos- transform.position ;
        body.velocity = dir * 5;
        endposition = endpos;   
    }
    public void SetState(PlayerState newState)
    {
        state = newState;

        switch (state)
        {
            case PlayerState.Default:
                body.useGravity = true;
                break;
            case PlayerState.Pulling:
                body.useGravity = false;
                break;
            default:
                break;
        }
    }
    void HandleRotation()
    {

        float MouseX = Input.GetAxisRaw("Mouse X") * MouseXSensitivity;
        float MouseY = Input.GetAxisRaw("Mouse Y") * MouseYSensitivity;

        YRotation += MouseX;
        XRotation -= MouseY;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

 //       transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        transform.rotation = Quaternion.Euler(0, YRotation, 0);
        orient.rotation = Quaternion.Euler(0, YRotation, 0);
        bodycam.transform.rotation = Quaternion.Euler(XRotation, bodycam.transform.rotation.eulerAngles.y, bodycam.transform.rotation.eulerAngles.z);
    }
}
