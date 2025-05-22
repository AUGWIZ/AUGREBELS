using UnityEngine;
using System.Collections.Generic;

public class ScreenResolutionScale : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> uiElements;

    // Booleans and scale values for different resolution categories
    [SerializeField, Tooltip("Apply scaling for Widescreen resolutions with aspect ratio >= 1.8 (e.g., 16:9, 1920x1080, 2560x1440)")]
    private bool applyWidescreenScale = false;

    [SerializeField] private Vector3 widescreenScale = new Vector3(1f, 1f, 1f);

    [SerializeField, Tooltip("Apply scaling for 20:9 resolutions (e.g., Ultra-Widescreen, 2560x1080, 3840x1600)")]
    private bool applyUltraWideScreenScale = false;

    [SerializeField] private Vector3 ultraWideScreenScale = new Vector3(1.4f, 1f, 1f); // Example scale for 20:9 aspect ratio

    [SerializeField, Tooltip("Apply scaling for Balanced Tablet resolutions with aspect ratio >= 1.7 and < 1.8 (e.g., 3:2, 2048x1536, 1600x1200)")]
    private bool applyTabletScale = false;

    [SerializeField] private Vector3 tabletScale = new Vector3(1.2f, 1.2f, 1f);

    [SerializeField, Tooltip("Apply scaling for Mid-sized Tablet resolutions with aspect ratio >= 1.5 and < 1.7 (e.g., 4:3, 2048x1536, 1280x960)")]
    private bool applyTabletScaleMid = false;

    [SerializeField] private Vector3 tabletScale_Mid = new Vector3(1.1f, 1.1f, 1f);

    [SerializeField, Tooltip("Apply scaling for Square-like or Portrait Tablet resolutions with aspect ratio >= 1 and < 1.5 (e.g., 1:1, 1080x1080, 1024x1024)")]
    private bool applySquareLikeOrPortraitTabletScale = false;

    [SerializeField] private Vector3 SquareLikeOrPortraitTabletScale = new Vector3(1.3f, 1.3f, 1f);

    [Space]
    [Header("____________ PORTRAIT ____________")]
    [SerializeField, Tooltip("Apply scaling for Portrait mode resolutions with aspect ratio < 1 (e.g., 9:16, 1080x1920, 750x1334)")]
    private bool applyPortraitScale = false;

    [SerializeField] private Vector3 portraitScale = new Vector3(0.8f, 0.8f, 1f);
    
    [TextArea(5, 15)] 
    [SerializeField]
    private string notes;

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
        if (aspectRatio >= 2.2f) // Ultra-Widescreen (20:9 or similar)
        {
            HandleUltraWideScreen(screenWidth, screenHeight);
        }
        else if (aspectRatio >= 1.8f && aspectRatio < 2.2f) // Widescreen (16:9 or similar)
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

        if (applyWidescreenScale && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(widescreenScale);
        }
    }

    void HandleUltraWideScreen(int width, int height)
    {
        Debug.Log("Resolution Category: 20:9 Ultra-Widescreen");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyUltraWideScreenScale && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(ultraWideScreenScale);
        }
    }

    void HandleBalancedTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyTabletScale && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(tabletScale);
        }
    }

    void HandleBalancedTabletMid(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyTabletScaleMid && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(tabletScale_Mid);
        }
    }

    void HandleSquareLikeOrPortraitTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Square-like or Portrait Tablet");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applySquareLikeOrPortraitTabletScale && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(SquareLikeOrPortraitTabletScale);
        }
    }

    void HandlePortrait(int width, int height)
    {
        Debug.Log("Resolution Category: Portrait (Tall)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);

        if (applyPortraitScale && uiElements != null && uiElements.Count > 0)
        {
            ApplyUIScale(portraitScale);
        }
    }

    // Apply scale to UI elements based on the resolution category
    void ApplyUIScale(Vector3 scaleValue)
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                // Set the scale of each UI element based on the selected scale value
                uiElement.transform.localScale = scaleValue;

                // Optionally, log the scaled UI element for debugging
                Debug.Log("Scaled " + uiElement.name + " to: " + scaleValue);
            }
        }
    }
}
