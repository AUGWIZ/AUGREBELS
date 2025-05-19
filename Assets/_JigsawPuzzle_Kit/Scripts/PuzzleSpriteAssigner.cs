using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SpriteGroup
{
    public List<Sprite> sprites;
}

public class PuzzleSpriteAssigner : MonoBehaviour
{
    [Header("List of Sprite Renderers to Assign To")]
    [SerializeField] private List<SpriteRenderer> spriteRenderers;

    [Header("Groups of Sprites")]
    [SerializeField] private List<SpriteGroup> spriteGroups = new List<SpriteGroup>();

    [Header("Index of the Sprite Group to Use")]
    [SerializeField] private int groupIndex = 0;

    [SerializeField] private string rebelName = "";
    
    public GameController gameController;
    void Start()
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("RebelName")))
        {
            rebelName = PlayerPrefs.GetString("RebelName");
        }
        
        gameController.ResetPuzzle();
        gameController.RestartPuzzle();
        
        switch (rebelName)
        {
            case "MaryKom":
                groupIndex = 0;
                break;

            case "MaryKom1":
                groupIndex = 1;
                break;

            case "Amelia":
                groupIndex = 2;
                break;

            case "Amelia1":
                groupIndex = 3;
                break;

            case "JK":
                groupIndex = 4;
                break;

            case "JK1":
                groupIndex = 5;
                break;

            case "Marie":
                groupIndex = 6;
                break;

            case "Marie1":
                groupIndex = 7;
                break;

            case "Indra":
                groupIndex = 8;
                break;
            
            case "Indra1":
                groupIndex = 9;
                break;
            
            
            
        }
        AssignSprites(groupIndex);
    }

    private void AssignSprites(int index)
    {
        if (index < 0 || index >= spriteGroups.Count)
        {
            Debug.LogWarning("Invalid group index.");
            return;
        }

        List<Sprite> selectedGroup = spriteGroups[index].sprites;

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            if (i < selectedGroup.Count)
            {
                spriteRenderers[i].sprite = selectedGroup[i];
            }
            else
            {
                Debug.LogWarning("Not enough sprites in the selected group for all SpriteRenderers.");
                break;
            }
        }
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}