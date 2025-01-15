using UnityEngine;

public class DoorInteractable : MonoBehaviour, Interactable {
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private Animator playerAnimator;
    private string[] interactionText = { "Open Door", "Close Door" };
    private bool doorOpen;
    
    public void Interact() {
        if (doorOpen) {
            doorAnimator.Play("DoorClose");
            playerAnimator.SetTrigger("OpenDoorTrigger");
            doorOpen = false;
        } else {
            doorAnimator.Play("DoorOpen");
            playerAnimator.SetTrigger("OpenDoorTrigger");
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