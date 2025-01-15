using UnityEngine;

public class AnimatedInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string[] animationTrigger;
    [SerializeField] private Animator[] animator;

    public void Interact() {
        for (int i = 0; i < animationTrigger.Length; i++) {
            animator[i].SetTrigger(animationTrigger[i]);
        }
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}