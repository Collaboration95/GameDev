using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    // Assign your primary camera in the Inspector
    public Camera activeCamera;

    // Optionally, assign any extra cameras you might want to disable
    public Camera[] otherCameras;

    void OnEnable()
    {
        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Enable the camera you want active
        if (activeCamera != null)
        {
            activeCamera.enabled = true;
        }

        // Disable any other cameras in the scene
        foreach (Camera cam in otherCameras)
        {
            if (cam != null)
            {
                cam.enabled = false;
            }
        }
    }
}
