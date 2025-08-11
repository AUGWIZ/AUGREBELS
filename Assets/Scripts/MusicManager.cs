using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips; // List to hold audio clips
    private int currentClipIndex = 0; // Index of the current clip being played
    private float musicTime = 0f; // Track the music playback time
    
    // Volume settings
    [SerializeField] private float volumeChangeAmount = 0.1f; // The amount by which volume changes
    [SerializeField] private float minVolume = 0f; // Minimum volume (mute)
    [SerializeField] private float maxVolume = 1f; // Maximum volume (full)
    
    private float previousVolume = 1f; // Variable to store the previous volume before mute

    void Awake()
    {
        // Ensure only one instance of the MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this GameObject on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate if it exists
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // Ensure we have audio clips in the list
        if (audioClips.Count == 0)
        {
            Debug.LogError("No audio clips assigned in the MusicManager.");
        }
    }

    void Start()
    {
        // Start playing the default music (first clip)
        PlayClip(currentClipIndex);
    }

    void Update()
    {
        // Track the current music time to resume from it later
        if (audioSource.isPlaying)
        {
            musicTime = audioSource.time;
        }
    }

    public void PlayClip(int clipIndex)
    {
        // Ensure clipIndex is within the range of the list
        if (clipIndex < 0 || clipIndex >= audioClips.Count)
        {
            Debug.LogError("Invalid audio clip index.");
            return;
        }

        // If the clip is the same as the current one, continue from where we left off
        if (audioClips[clipIndex] == audioSource.clip)
        {
            // Continue playing from the current position
            audioSource.time = musicTime;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Switch to the new clip and play from the start
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
            musicTime = 0f; // Reset time when switching to a new clip
        }

        currentClipIndex = clipIndex; // Update the current clip index
    }

    public void StopMusic()
    {
        // Stop the music and save the current time
        if (audioSource.isPlaying)
        {
            musicTime = audioSource.time;
            audioSource.Stop();
        }
    }

    public void NextClip()
    {
        // Play the next clip in the list (looping if needed)
        currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
        PlayClip(currentClipIndex);
    }

    public void PreviousClip()
    {
        // Play the previous clip in the list (looping if needed)
        currentClipIndex = (currentClipIndex - 1 + audioClips.Count) % audioClips.Count;
        PlayClip(currentClipIndex);
    }

    // Method to increase volume
    public void IncreaseVolume()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume + volumeChangeAmount, minVolume, maxVolume);
    }

    // Method to decrease volume
    public void DecreaseVolume()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume - volumeChangeAmount, minVolume, maxVolume);
    }

    // Method to mute the audio
    public void Mute()
    {
        // Store the current volume before muting
        previousVolume = audioSource.volume;
        audioSource.volume = 0f; // Mute the audio
       // audioSource.mute = true;
    }

    // Method to unmute the audio
    public void Unmute()
    {
        audioSource.mute = false;
        audioSource.volume = 1f; // Restore the previous volume
    }
}
