using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBtn : MonoBehaviour
{

    public void ExitGameScene(string sceneName)
    {
        MusicManager.instance.Unmute();
        SceneManager.LoadScene(sceneName);
    }
    
}
