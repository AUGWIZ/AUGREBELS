using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using SpotTheDifference;

public class SlidePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform panel;
    public Button toggleButton;
    private bool isPanelVisible = false;
    public float slideDuration = 0.5f;
    public float spinDuration = 0.2f;
    public float spinAngle = 180f;

    public GameObject SpotTheDifferenceDetection;

    private bool isHovering = false;

    private void Start()
    {
        toggleButton.onClick.AddListener(TogglePanel);
    }

    public void TogglePanel()
    {
        if (isPanelVisible)
        {
            SpotTheDifferenceDetection.SetActive(true); 
            SlidePanelDown();
            SpinButton(-spinAngle);
        }
        else
        {
            SpotTheDifferenceDetection.SetActive(false); 
            SlidePanelUp();
            SpinButton(spinAngle);
        }

        isPanelVisible = !isPanelVisible;
    }

    private void SlidePanelUp()
    {
        Vector2 targetPosition = new Vector2(panel.anchoredPosition.x, 200f);
        panel.DOAnchorPos(targetPosition, slideDuration);
    }

    private void SlidePanelDown()
    {
        Vector2 targetPosition = new Vector2(panel.anchoredPosition.x, -900f);
        panel.DOAnchorPos(targetPosition, slideDuration);
    }

    private void SpinButton(float angle)
    {
        toggleButton.transform.DORotate(new Vector3(0, 0, angle), spinDuration, RotateMode.LocalAxisAdd);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SpotTheDifferenceDetection.SetActive(false); // Hide when hovering
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SpotTheDifferenceDetection.SetActive(true); // Show when not hovering
    }
}