using UnityEngine;

public class InputHandle
{

    public bool GetInteractInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.X);
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button1);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button1);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button1);
            default:
                return false;
        }
    }

    public bool GetAttackInput(int playerNo)
    {
        switch (playerNo)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.Z);
            case 1:
                return Input.GetKeyDown(KeyCode.Joystick1Button1);
            case 2:
                return Input.GetKeyDown(KeyCode.Joystick2Button1);
            case 3:
                return Input.GetKeyDown(KeyCode.Joystick3Button1);
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
