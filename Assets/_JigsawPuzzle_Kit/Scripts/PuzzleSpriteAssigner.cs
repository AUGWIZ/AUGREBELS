using UnityEngine;
using System.Collections.Generic;

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

    void Start()
    {
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
}