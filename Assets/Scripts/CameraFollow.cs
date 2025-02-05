// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     [Header("Target to Follow")]
//     public Transform target;

//     [Header("Follow Settings")]
//     [Tooltip("How quickly the camera catches up to the target.")]
//     public float followSpeed = 5f;

//     [Tooltip("Offset from the target position (e.g., (0, 1, -10) means 1 unit above and 10 units behind the target).")]
//     public Vector3 offset = new Vector3(0f, 1f, -10f);

//     // LateUpdate is called after all Update functions have been called.
//     void LateUpdate()
//     {
//         if (target != null)
//         {
//             // Determine the desired position by adding the offset to the target's position.
//             Vector3 desiredPosition = target.position + offset;

//             // Smoothly interpolate from the current camera position to the desired position.
//             Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

//             // Update the camera's position.
//             transform.position = smoothedPosition;
//         }
//     }
// }
