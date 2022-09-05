using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputGame : MonoBehaviour
{
    public Vector2 moveDirection;
    public Vector2 cameraMoveDirection;
    public Vector2 CameraRotation { get; private set; }

    private Vector2 attackBtns;
    public Vector2 AttackBtns
    {
        get { return attackBtns; }
        set
        {
            attackBtns = value;
        }
    }

    public PlayerMovement PlayerMovement { get; set; }

    public void AttackButtons(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (MathF.Round(context.ReadValue<Vector2>().x) == 1)
            {
                AttackBtns = Vector2.right;
            }
            else if (MathF.Round(context.ReadValue<Vector2>().x) == -1)
            {
                AttackBtns = Vector2.left;
            }
            else if (MathF.Round(context.ReadValue<Vector2>().y) == 1)
            {
                AttackBtns = Vector2.up;
            }
            else if (MathF.Round(context.ReadValue<Vector2>().y) == -1)
            {
                AttackBtns = Vector2.down;
            }
            else
            {
                AttackBtns = Vector2.zero;
            }
        }

        if (context.canceled)
        {
            AttackBtns = Vector2.zero;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (PlayerMovement)
            {
                PlayerMovement.Walking(context.ReadValue<Vector2>());
            }
        }

        if (context.canceled)
        {
            if (PlayerMovement)
            {
                PlayerMovement.Walking(new Vector2(0, 0));
                PlayerMovement.StopWalking();
            }
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.OnUpdateGameState(GameStates.Pause);
        }
    }

    public void SetCameraMoveDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cameraMoveDirection = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            cameraMoveDirection = Vector2.zero;
        }
    }

    public void SetCameraRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CameraRotation = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            CameraRotation = Vector2.zero;
        }
    }
}
