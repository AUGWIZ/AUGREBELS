using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleSceneManager : MonoBehaviour
{
   [Header("Main Scene")]
   public GameObject playBG;
   public GameObject SelectPuzzle;
    
   [Header("Puzzle Scene")]
   public GameObject finishPuzzle;

   private void Start()
   {
      if (finishPuzzle != null)
      {
         finishPuzzle.SetActive(false);
      }
   }

   public void PlayPuzzle()
   {
      playBG.SetActive(false);
      SelectPuzzle.SetActive(true);
   }

   public void SelectPuzzleScene(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
      
   }

   public void LoadScene(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
      
   }

   public void CloseFinishedPuzzle()
   {
      finishPuzzle.SetActive(false);
   }
}
