using UnityEngine;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{

    // public void OnMove(InputValue input)
    // {
    //     if (input.Get() == null)
    //     {
    //         Debug.Log("Move released");
    //     }
    //     else
    //     {
    //         Debug.Log($"Move triggered, with value {input.Get()}"); // will return null when released
    //     }
    //     // TODO
    // }
    // // This callback is automatically called when the "Jump" action is triggered.
    // // The "Jump" action is expected to be a Button (bool) control.
    // public void OnJump(InputValue value)
    // {
    //     bool isJumping = value.isPressed;
    //     Debug.Log("Jump action triggered: " + isJumping);
    //     // Insert additional logic for Mario's jump here.
    // }

    // // Add more callback methods matching your input action names
    // // For example, if you have an "Attack" action:
    // public void OnAttack(InputValue value)
    // {
    //     bool isAttacking = value.isPressed;
    //     Debug.Log("Attack action triggered: " + isAttacking);
    //     // Insert logic for attacking here.
    // }
    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("JumpHold was started");
        else if (context.performed)
        {
            Debug.Log("JumpHold was performed");
        }
        else if (context.canceled)
            Debug.Log("JumpHold was cancelled");
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Jump was started");
        else if (context.performed)
        {
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
            Debug.Log("Jump was cancelled");

    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("move started");
            float move = context.ReadValue<float>();
            Debug.Log($"move value: {move}"); // will return null when not pressed
        }
        if (context.canceled)
        {
            Debug.Log("move stopped");
        }
    }
}
