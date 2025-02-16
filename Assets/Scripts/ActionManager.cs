using UnityEngine;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{

    public void OnMove(InputValue input)
    {
        if (input.Get() == null)
        {
            Debug.Log("Move released");
        }
        else
        {
            Debug.Log($"Move triggered, with value {input.Get()}"); // will return null when released
        }
        // TODO
    }
    // This callback is automatically called when the "Jump" action is triggered.
    // The "Jump" action is expected to be a Button (bool) control.
    public void OnJump(InputValue value)
    {
        bool isJumping = value.isPressed;
        Debug.Log("Jump action triggered: " + isJumping);
        // Insert additional logic for Mario's jump here.
    }

    // Add more callback methods matching your input action names
    // For example, if you have an "Attack" action:
    public void OnAttack(InputValue value)
    {
        bool isAttacking = value.isPressed;
        Debug.Log("Attack action triggered: " + isAttacking);
        // Insert logic for attacking here.
    }
}
