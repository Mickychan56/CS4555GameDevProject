using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Transform player;
    public Camera yourCam;

    private PlayerInput InputActions;
    private Vector2 movementInput;
    Animator anim;

    [SerializeField]
    public float walk = 2f;
    public float run = 7f;
    private float speed = 10;

    private Vector3 inputDirection;
    private Vector3 moveVector;
    private Quaternion currentRotation;
    public static bool aiming = false;

    private void Awake()
    {
        InputActions = new PlayerInput();
        anim = GetComponent<Animator>();
        InputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float h = movementInput.x;
        float v = movementInput.y;

        Vector3 targetInput = new Vector3(h, 0, v);

        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

        Vector3 canForward = Camera.main.transform.forward;
        Vector3 canRight = Camera.main.transform.right; 
        canForward.y = 0f;
        canRight.y = 0f;

        Vector3 desiredDirection = canForward * inputDirection.z + canRight * inputDirection.x; // Direction youre moving towards

        Move(desiredDirection);
        if (!aiming)
        {
            // Player turns towards direction running
            Turn(desiredDirection);
        } else
        {
            // Player turns towards direction of the camera is pointing
            TurnAim();
        }

        if (Input.GetMouseButtonDown(1))
        {
            aiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            aiming = false;
        }
    }

    void Move(Vector3 desiredDirection)
    {
        moveVector.Set(desiredDirection.x, 0f, desiredDirection.z);

        if (anim.GetFloat("Speed") < 1f || PlayerStats.CurrentStamina == 0)
            speed = walk;
        else
            speed = run;

        moveVector = moveVector * speed * Time.deltaTime;
        transform.position += moveVector;
    }

    void Turn(Vector3 desiredDirection)
    {
        //Turns inot the direction the player is going
        if ((desiredDirection.x > 0.1 || desiredDirection.x < -0.1) || (desiredDirection.z > 0.1 || desiredDirection.z < -0.1))
        {
            currentRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = currentRotation;
        }
        else
        {
            transform.rotation = currentRotation;
        }
    }

    void TurnAim()
    {
        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0;
        if (fwd.sqrMagnitude != 0.0f)
        {
            fwd.Normalize();
            transform.LookAt(transform.position + fwd);
        }
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
