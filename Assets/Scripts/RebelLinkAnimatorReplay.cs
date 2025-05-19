using UnityEngine;

public class RebelLinkAnimatorReplay : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (animator != null)
        {
            // Forcefully play from the beginning
            animator.Play("2_ChoosePuzzle", 0, 0f);
        }
        else
        {
            Debug.LogError("Animator is not assigned!");
        }
    }
}