// AudioController.cs
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string normalSnapshotName = "MainTheme";
    [SerializeField] private string dimmedSnapshotName = "GameOver";
    [SerializeField] private float transitionTime = 0.5f;

    private AudioMixerSnapshot normalSnapshot;
    private AudioMixerSnapshot dimmedSnapshot;

    private void Awake()
    {
        if (audioMixer)
        {
            normalSnapshot = audioMixer.FindSnapshot(normalSnapshotName);
            dimmedSnapshot = audioMixer.FindSnapshot(dimmedSnapshotName);
        }
    }



    public void OnGameRestart()
    {
        normalSnapshot?.TransitionTo(transitionTime);
        Debug.Log("MainTheme");
    }

    public void OnGameOver()
    {
        dimmedSnapshot?.TransitionTo(transitionTime);
        Debug.Log("TransitionToGameOver");
    }
}
