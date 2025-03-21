using UnityEngine;

public class AnimatedInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string[] animationTriggers;
    [SerializeField] private Animator[] animators;

    public void Interact() {
        for (int i = 0; i < animationTriggers.Length; i++) {
            Debug.Log(animationTriggers[i]);
            animators[i].SetTrigger(animationTriggers[i]);
        }
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        Debug.Log(transform.rotation.eulerAngles);
        return transform;
    }
}