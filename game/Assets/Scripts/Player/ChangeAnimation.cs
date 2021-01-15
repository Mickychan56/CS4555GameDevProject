using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeAnimation : MonoBehaviour
{
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;

    Animator anim;
    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get animation controller
        if (PlayerStats.HasRifle)
        {
            anim.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        }
        else
        {
            anim.runtimeAnimatorController = anim1 as RuntimeAnimatorController;
        }

        var keyboard = Keyboard.current;

        if(keyboard != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = runToggle();
            }

            if (Input.GetKey(KeyCode.W)  || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (isRunning && PlayerStats.CurrentStamina > 0)
                    anim.SetFloat("Speed", 1f); 
                else
                    anim.SetFloat("Speed", 0.5f);
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetFloat("Speed", 0.0f);
            }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("Aiming", true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("Aiming", false);
            }
        }  
    }

    public void Jump()
    {
        //anim.SetBool("Squat", false);
        //anim.SetBool("Aiming", false);
        anim.SetTrigger("Jump");
    }

    private bool runToggle()
    {
        if (isRunning)
            return false;
        else
            return true;
    }
}
