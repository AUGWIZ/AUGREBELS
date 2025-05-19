using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [Header("Auto Load Settings")]
    public bool autoLoad = false;
    public float delay = 5f; 

    [Header("Scene Settings")]
    public string sceneToLoad;

    [Header("Game Chooses")]
    [SerializeField] private string currentGameName;
    
    [Header("Rebel Choosen")]
    [SerializeField] private string rebelChoosen;
    
    private void Start()
    {
        if (autoLoad && !string.IsNullOrEmpty(sceneToLoad))
        {
            Invoke(nameof(AutoLoadScene), delay);
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("GameName")))
        {
            currentGameName = PlayerPrefs.GetString("GameName");
        }

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("RebelName")))
        {
            rebelChoosen = PlayerPrefs.GetString("RebelName");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void AutoLoadScene()
    {
        LoadScene(sceneToLoad);
    }

    public void LoadChooseRebelScene(string gameName) //default laod choose rebel while storing game name
    {
        PlayerPrefs.SetString("GameName", gameName);
        currentGameName = gameName;
        SceneManager.LoadScene("5ChooseRebel");
    }
    
    public void LoadGameWithChoosenRebel(string rebelName) //default laod choose rebel while storing game name
    {
        PlayerPrefs.SetString("RebelName", rebelName);
        rebelChoosen = rebelName;
        SceneManager.LoadScene(currentGameName);
    }
}