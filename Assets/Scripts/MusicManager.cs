using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;
    private float pausedTime = 0f;
    private bool isPaused = false;

    public AudioClip musicClip;

    void Awake()
    {
        // Singleton pattern to persist this manager across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    void InitializeAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            if (isPaused)
            {
                audioSource.time = pausedTime;
                isPaused = false;
            }
            audioSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            pausedTime = audioSource.time;
            audioSource.Pause();
            isPaused = true;
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying || isPaused)
        {
            audioSource.Stop();
            pausedTime = 0f;
            isPaused = false;
        }
    }
}