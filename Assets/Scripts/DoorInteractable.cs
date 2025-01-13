using UnityEngine;

public class DoorInteractable : MonoBehaviour, Interactable {
    [SerializeField] private Animator doorAnimator;
    private string[] interactionText = { "Open Door", "Close Door" };
    private bool doorOpen;
    
    public void Interact() {
        if (doorOpen) {
            doorAnimator.Play("DoorClose");
            doorOpen = false;
        } else {
            doorAnimator.Play("DoorOpen");
            doorOpen = true;
        }
    }

    public string GetInteractionText() {
        return doorOpen ? interactionText[1] : interactionText[0];
    }

    public Transform GetTransform() {
        return transform;
    }
}