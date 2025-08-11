using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RebelReelManager : MonoBehaviour
{
    [Header("UI & Image")]
    [SerializeField] private List<Sprite> rebelImages;
    [SerializeField] private Image rebelImage;

    [Header("Rebel Name")]
    [SerializeField] private string rebelName;

    [Header("Video Player")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Videos - Beginning")]
    [SerializeField] private List<VideoClip> beginningVideos;

    [Header("Videos - Breaking Barrier")]
    [SerializeField] private List<VideoClip> breakingBarrierVideos;

    private Dictionary<string, int> rebelIndexMap;

    [SerializeField] private GameObject playHolder;
    [SerializeField] private GameObject videoPlayerRenderer;

    private void Start()
    {
        MusicManager.instance.Mute();
        
        playHolder.SetActive(true);
        videoPlayerRenderer.gameObject.SetActive(false);
        
        rebelName = PlayerPrefs.GetString("RebelName");
        InitializeRebelIndexMap();

        if (rebelIndexMap.ContainsKey(rebelName))
        {
            int index = rebelIndexMap[rebelName];
            rebelImage.sprite = rebelImages[index];
        }
        else
        {
            Debug.LogWarning("Rebel name not found in map: " + rebelName);
        }
    }

    private void InitializeRebelIndexMap()
    {
        rebelIndexMap = new Dictionary<string, int>
        {
            { "MaryKom", 0 },
            { "Amelia", 1 },
            { "JK", 2 },
            { "Marie", 3 },
            { "Indra", 4 }
        };
    }

    public void PlayBeginningVideo()
    {
        MusicManager.instance.Mute();
        playHolder.SetActive(false);
        videoPlayerRenderer.gameObject.SetActive(true);
        
        if (rebelIndexMap.TryGetValue(rebelName, out int index) && index < beginningVideos.Count)
        {
            videoPlayer.clip = beginningVideos[index];
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Beginning video not found for: " + rebelName);
        }
    }

    public void PlayBreakingBarrierVideo()
    {
        MusicManager.instance.Mute();
        playHolder.SetActive(false);
        videoPlayerRenderer.gameObject.SetActive(true);
        
        if (rebelIndexMap.TryGetValue(rebelName, out int index) && index < breakingBarrierVideos.Count)
        {
            videoPlayer.clip = breakingBarrierVideos[index];
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Breaking barrier video not found for: " + rebelName);
        }
    }

    public void ExitPlayer()
    {
              
        videoPlayer.Stop();
        playHolder.SetActive(true);
        videoPlayerRenderer.gameObject.SetActive(false);
    }
}
