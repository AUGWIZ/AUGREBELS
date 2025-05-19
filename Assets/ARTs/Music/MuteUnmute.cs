using UnityEngine;
using UnityEngine.UI;

public class MuteUnmute : MonoBehaviour
{
    [Header("Mute/Unmute Button")]
    [SerializeField] private Button muteUnmuteButton;
    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Sprite unmuteIcon;

    private bool isMuted = false;

    private const string MuteKey = "isMuted";

    private void Start()
    {
        // Check PlayerPrefs for the mute state and update accordingly
        isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMuted;
        }

        UpdateButtonImage();
    }

    public void ToggleMuteAllAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        isMuted = !isMuted;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMuted;
        }

        // Save mute state to PlayerPrefs
        PlayerPrefs.SetInt(MuteKey, isMuted ? 1 : 0);

        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        if (muteUnmuteButton != null)
        {
            Image buttonImage = muteUnmuteButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = isMuted ? muteIcon : unmuteIcon;
            }
        }
    }

    private void OnApplicationQuit()
    {
        // Clear PlayerPrefs when the game stops or exits
        PlayerPrefs.DeleteKey(MuteKey);
    }
}