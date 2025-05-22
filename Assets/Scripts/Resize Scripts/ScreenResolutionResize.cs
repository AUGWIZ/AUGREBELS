using UnityEngine;
using System.Collections.Generic;

public class ScreenResolutionResize : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> uiElements;  
    
    // Separate scale values for different resolution categories
    [SerializeField]
    private Vector3 widescreenScaleValue = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 tabletScaleValue = new Vector3(1.2f, 1.3f, 1.2f);
    [SerializeField] private Vector3 tabletScaleValue_Mid = new Vector3(1.2f, 1.3f, 1.2f);
    [SerializeField] private Vector3 SquareLikeOrPortraitTablet = new Vector3(1.23f, 1.55f, 1.23f);
    [SerializeField] private Vector3 portraitScaleValue = new Vector3(0.6f, 0.6f, 1f);

    
    [SerializeField] private bool isSmartphoneView = false;
    [Header("For Choose GAme Scene")] 
    [SerializeField] private bool isChooseGameView = false;

    [SerializeField] private RectTransform ChooseGameView;
        
        
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

        if (uiElements != null || uiElements.Count > 0)
        {
            if (isSmartphoneView)
            {
                ApplyUIScale(widescreenScaleValue);
            }
        }
    }

    void HandleBalancedTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);
        
        if (uiElements != null || uiElements.Count > 0)
        {
            ApplyUIScale(tabletScaleValue);
        }
        if (ChooseGameView != null && isChooseGameView)
        {
            ChooseGameView.anchoredPosition = new Vector2(ChooseGameView.anchoredPosition.x, -410f);
            ChooseGameView.sizeDelta = new Vector2(ChooseGameView.sizeDelta.x, 1000f);
        }

    }
    
    void HandleBalancedTabletMid(int width, int height)
    {
        Debug.Log("Resolution Category: Balanced Tablet (3:2 or 4:3)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);
        
        if (uiElements != null || uiElements.Count > 0)
        {
            ApplyUIScale(tabletScaleValue_Mid);
        }
        if (ChooseGameView != null && isChooseGameView)
        {
            ChooseGameView.anchoredPosition = new Vector2(ChooseGameView.anchoredPosition.x, -410f);
            ChooseGameView.sizeDelta = new Vector2(ChooseGameView.sizeDelta.x, 1000f);
        }

    }
    
    void HandleSquareLikeOrPortraitTablet(int width, int height)
    {
        Debug.Log("Resolution Category: Square-like or Portrait Tablet");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);
         
        if (uiElements != null || uiElements.Count > 0)
        {
            ApplyUIScale(SquareLikeOrPortraitTablet);
        }

        if (ChooseGameView != null && isChooseGameView)
        {
            ChooseGameView.anchoredPosition = new Vector2(ChooseGameView.anchoredPosition.x, -360f);
            ChooseGameView.sizeDelta = new Vector2(ChooseGameView.sizeDelta.x, 1100f);
        }
            
    }

    void HandlePortrait(int width, int height)
    {
        Debug.Log("Resolution Category: Portrait (Tall)");
        Debug.Log("Screen Width: " + width + ", Screen Height: " + height);
        
        if (uiElements != null || uiElements.Count > 0)
        {
          //  ApplyUIScale(portraitScaleValue);
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
