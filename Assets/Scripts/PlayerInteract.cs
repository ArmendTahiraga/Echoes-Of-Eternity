using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    public void LateUpdate() { // Made it late update to see if it fixes the animation teleport glitch
        if (Input.GetKeyDown(KeyCode.E) && !PauseMenuController.isGamePaused) {
            Interactable interactable = GetInteractable();
            if (interactable != null) {
                interactable.Interact();
                if (interactable.GetType() == typeof(NPCInteractable) || interactable.GetType() == typeof(MiniGameInteractable)) { 
                    GetComponent<PlayerMovement>().isPlayerMoving = false;
                    GameObject.Find("PlayerCam").GetComponent<PlayerCam>().lockCamera = true;
                }
            }
        }
    }

    public Interactable GetInteractable() {
        List<Interactable> interactableList = new List<Interactable>();
        float interactRange = 2f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliders) {
            if (collider.TryGetComponent(out Interactable interactable)) {
                interactableList.Add(interactable);
            }
        }

        Interactable closestInteractable = null;
        foreach (Interactable interactable in interactableList) {
            if (closestInteractable == null) {
                closestInteractable = interactable;
            } else {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position)) {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
}