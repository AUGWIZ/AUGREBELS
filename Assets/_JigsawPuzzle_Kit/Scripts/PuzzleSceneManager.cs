using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject maryKom;
    [SerializeField] private GameObject amelia;
    [SerializeField] private GameObject jk;
    [SerializeField] private GameObject marie;
    [SerializeField] private GameObject indra;
    
    [SerializeField] private string rebelName = "";

    private void Start()
    {
        Time.timeScale = 1;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("RebelName")))
        {
            rebelName = PlayerPrefs.GetString("RebelName");
        }
    }
    
    
    
    public void EnableObjectByName()
    {
        switch (rebelName)
        {
            case "MaryKom":
                if (maryKom != null) maryKom.SetActive(true);
                break;

            case "Amelia":
                if (amelia != null) amelia.SetActive(true);
                break;

            case "JK":
                if (jk != null) jk.SetActive(true);
                break;

            case "Marie":
                if (marie != null) marie.SetActive(true);
                break;

            case "Indra":
                if (indra != null) indra.SetActive(true);
                break;
            
            case "MaryKom1":
                if (maryKom != null) maryKom.SetActive(true);
                break;

            case "Amelia1":
                if (amelia != null) amelia.SetActive(true);
                break;

            case "JK1":
                if (jk != null) jk.SetActive(true);
                break;

            case "Marie1":
                if (marie != null) marie.SetActive(true);
                break;

            case "Indra1":
                if (indra != null) indra.SetActive(true);
                break;

            default:
                Debug.LogWarning($"No GameObject found for name: {name}");
                break;
        }
    }

    public void LoadPuzzleGame(int gameIndex)
    {
        if (gameIndex == 1)
        {
            PlayerPrefs.SetString("RebelName", rebelName + gameIndex);
        }
        else
        {
            PlayerPrefs.SetString("RebelName", rebelName);
        }
        SceneManager.LoadScene("Puzzle");
    }
}
