using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace SpotTheDifference
{
    public class SpotDifferenceDetection : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera; // Main camera to cast the ray from

        private int score = 0;

        public GraphicRaycaster raycaster;
        public EventSystem eventSystem;
        private int previousIndex = 0;

        // Update is called once per frame
        void Update()
        {
            // Check for a mouse click or touch input
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("case");
                // DetectBoxColliderAtClick();
                // Check if the pointer is NOT over a UI element (button)

                if (!IsPointerOverSpecificUI())
                {
                    Debug.Log("Clicked outside buttons!");

                    if (UIController.Instance.ScreenType == DataModel.ScreenType.GameScreen)
                    {
                        UIController.Instance.Chances();
                    }
                }
            }
        }

        // This function checks if the pointer is over a UI element
        private bool IsPointerOverSpecificUI()
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            raycaster.Raycast(pointerData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                // Check if the clicked object is a button (or any other specific UI element)
                if (result.gameObject.CompareTag("Difference"))  // Make sure buttons have a "Button" tag or check other component
                {
                    Debug.Log("Clicked on button: " + result.gameObject.name);
                    return true;
                }
                else if (result.gameObject.CompareTag("NonSpotArea")) // Specific tag for non-spot buttons/areas
                {
                    Debug.Log("Clicked on NonSpotArea: " + result.gameObject.name);
                    return true; // Prevent decreasing chances in this area
                }
            }

            return false;
        }
    }
}
