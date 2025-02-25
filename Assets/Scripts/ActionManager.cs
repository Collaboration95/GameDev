using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;

    private bool CanPrint = false; // Set this flag to false to disable printing
    // override public void Awake()
    // {
    //     base.Awake();
    //     Debug.Log("ActionManager Awake Called");

    // }

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Print("JumpHold was started");
        else if (context.performed)
        {
            Print("JumpHold was performed");
            Print(context.duration.ToString());
            jumpHold.Invoke();
        }
        else if (context.canceled)
            Print("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Print("Jump was started");
        else if (context.performed)
        {
            jump.Invoke();
            Print("Jump was performed");
        }
        else if (context.canceled)
            Print("Jump was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        // Print("OnMoveAction callback invoked");
        if (context.started)
        {
            Print("move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            Print("move stopped");
            moveCheck.Invoke(0);
        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Print("mouse click started");
        else if (context.performed)
        {
            Print("mouse click performed");
        }
        else if (context.canceled)
            Print("mouse click cancelled");
    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            Print($"Point detected: {point}");
        }
    }

    private void Print(string message)
    {
        if (CanPrint)
            Debug.Log(message);
    }
}
