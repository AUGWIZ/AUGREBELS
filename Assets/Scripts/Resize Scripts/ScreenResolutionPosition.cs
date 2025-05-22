using UnityEngine;
using System.Collections.Generic;

public class ScreenResolutionPosition : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> uiElements;  

    // Booleans and position values for different resolution categories
    [SerializeField] private bool applyWidescreenPosition = false;
    [SerializeField] private Vector3 widescreenPosition = new Vector3(0f, 0f, 0f);

    [SerializeField] private bool applyTabletPosition = false;
    [SerializeField] private Vector3 tabletPosition = new Vector3(0f, -50f, 0f);

    [SerializeField] private bool applyTabletPositionMid = false;
    [SerializeField] private Vector3 tabletPosition_Mid = new Vector3(0f, -100f, 0f);

    [SerializeField] private bool applySquareLikeOrPortraitTabletPosition = false;
    [SerializeField] private Vector3 SquareLikeOrPortraitTabletPosition = new Vector3(0f, -150f, 0f);

    [SerializeField] private bool applyPortraitPosition = false;
    [SerializeField] private Vector3 portraitPosition = new Vector3(0f, -200f, 0f);

    void Start()
    {
        DetermineResolutionCategory();
    }

    void DetermineResolutionCategory()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float aspectRatio = (float)screenWidth / screenHeight;

        // Call specific methods based on aspect ratio to classify the resolution
        if (aspectRatio >= 1.8f) // Widescreen (16:9 or similar)
        {
            HandleWidescreen(screenWidth, screenHeight);
        }
        else if (aspectRatio >= 1.7f && aspectRatio < 1.8f) // Balanced Tablets (3:2, 4:3)
        {
            HandleBalancedTablet(screenWidth, screenHeight);
        }
        else if (aspectRatio >= 1.5f && aspectRatio < 1.7f) // Slightly square [ 1.3, 1.49]
        {
            HandleBalancedTabletMid(screenWidth, screenHeight);
        }
        else if (aspectRatio >= 1f && aspectRatio < 1.5f) // Slightly square [1,1.1, 1.29]
        {
            HandleSquareLikeOrPortraitTablet(screenWidth, screenHeight);
        }
        else if (aspectRatio < 1f) // Portrait mode
        {
            HandlePortrait(screenWidth, screenHeight);
        }
        else
        {
            Debug.Log("Uncategorized Resolution");
        }
    }

    void HandleWidescreen(int width, int height)
    {
        Debug.Log("Resolution Category: Widescreen (Landscape)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyWidescreenPosition && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIPosition(widescreenPosition);
        }
    }

    void HandleBalancedTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyTabletPosition && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIPosition(tabletPosition);
        }
    }

    void HandleBalancedTabletMid(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyTabletPositionMid && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIPosition(tabletPosition_Mid);
        }
    }

    void HandleSquareLikeOrPortraitTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Square-like or Portrait Tablet");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applySquareLikeOrPortraitTabletPosition && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIPosition(SquareLikeOrPortraitTabletPosition);
        }
    }

    void HandlePortrait(int width, int height)
    {
        Debug.Log("Resolution Category: Portrait (Tall)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyPortraitPosition && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIPosition(portraitPosition);
        }
    }

    // Apply position to UI elements based on the resolution category
    void ApplyUIPosition(Vector3 positionValue)
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                // Set the position of each UI element based on the selected position value
                RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.localPosition = positionValue;
                }

                // Optionally, log the positioned UI element for debugging
                Debug.Log("Positioned " + uiElement.name + " to: " + positionValue);
            }
        }
    }
}
