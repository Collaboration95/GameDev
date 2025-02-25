using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Snapshot Names")]
    [SerializeField] private string normalSnapshotName = "MainTheme";
    [SerializeField] private string dimmedSnapshotName = "GameOver";

    [Header("Volume Parameter")]
    [Tooltip("Must match the exposed parameter name in the Audio Mixer.")]
    [SerializeField] private string exposedVolumeParam = "BackgroundVolume";

    [Header("Volume Levels (in dB)")]
    [SerializeField] private float normalVolume = 0f;
    [SerializeField] private float mutedVolume = -80f;

    [Header("Transition")]
    [SerializeField] private float transitionTime = 0.5f;

    private AudioMixerSnapshot normalSnapshot;
    private AudioMixerSnapshot dimmedSnapshot;

    public void Awake()
    {
        if (audioMixer)
        {
            normalSnapshot = audioMixer.FindSnapshot(normalSnapshotName);
            dimmedSnapshot = audioMixer.FindSnapshot(dimmedSnapshotName);
        }

        GameManager.instance.gameRestart.AddListener(OnGameRestart);
        GameManager.instance.gameOver.AddListener(OnGameOver);
    }

    public void OnGameRestart()
    {
        audioMixer.SetFloat(exposedVolumeParam, normalVolume);
        normalSnapshot?.TransitionTo(transitionTime);
        Debug.Log("TransitionToMainTheme + UnmuteVolume");
    }

    public void OnGameOver()
    {
        audioMixer.SetFloat(exposedVolumeParam, mutedVolume);
        dimmedSnapshot?.TransitionTo(transitionTime);

        Debug.Log("TransitionToGameOver + MuteVolume");
    }
}
