using UnityEngine;
using UnityEngine.Audio;

public class PitchShiftController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement; // Drag in your PlayerMovement object
    [SerializeField] private AudioMixer audioMixer;         // The mixer with the Pitch Shifter effect

    [Header("Pitch Parameters")]
    [Tooltip("Must match the exposed Pitch parameter in the Audio Mixer")]
    [SerializeField] private string exposedPitchParam = "MusicPitch";

    [Tooltip("Pitch when on the ground (1.0 = normal, no shift)")]
    [SerializeField] private float groundPitch = 1.0f;

    [Tooltip("Pitch when in the air (e.g., 0.8 = slightly lower)")]
    [SerializeField] private float airPitch = 0.8f;

    [Tooltip("How quickly the pitch transitions")]
    [SerializeField] private float pitchTransitionSpeed = 2f;

    // We'll smoothly blend from one pitch to another
    private float currentPitch;

    private void Start()
    {
        // Initialize the pitch to the "ground" value
        currentPitch = groundPitch;
        audioMixer.SetFloat(exposedPitchParam, currentPitch);
    }

    private void Update()
    {
        // Check if Mario is on the ground
        bool isGrounded = IsMarioGrounded();

        // Decide the target pitch
        float targetPitch = isGrounded ? groundPitch : airPitch;

        // Smoothly lerp pitch (optional for a gradual transition)
        currentPitch = Mathf.Lerp(
            currentPitch,
            targetPitch,
            Time.deltaTime * pitchTransitionSpeed
        );

        // Apply the pitch to the Audio Mixer
        audioMixer.SetFloat(exposedPitchParam, currentPitch);
    }

    private bool IsMarioGrounded()
    {

        return playerMovement.IsGrounded();
    }
}
