﻿using UnityEngine;

public class InputHandle
{

    public bool GetInteractInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1);
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick2Button2);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button1) || Input.GetKeyDown(KeyCode.Joystick3Button2);
            default:
                return false;
        }
    }

    public bool GetAttackInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0);
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button0);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button0);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button0);
            default:
                return false;
        }
    }

    public float GetHorizontalInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetAxisRaw("Horizontal");
            case 1:
                return Input.GetAxis("Horizontal1");
            case 2:
                return Input.GetAxis("Horizontal2");
            case 3:
                return Input.GetAxis("Horizontal3");
            default:
                return 0f;
        }
    }

    public float GetVerticalInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetAxisRaw("Vertical");
            case 1:
                return Input.GetAxis("Vertical1");
            case 2:
                return Input.GetAxis("Vertical2");
            case 3:
                return Input.GetAxis("Vertical3");
            default:
                return 0f;
        }
    }

}
