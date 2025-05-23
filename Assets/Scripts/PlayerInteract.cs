using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    [SerializeField] private GameObject miniGame;
    [SerializeField] private float interactRange = 2f;
    
    public void Update() {
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

        if (miniGame) {
            switch (miniGame.GetComponent<MiniGame>().GetMiniGameResult()) { 
                case "Success":
                    Interactable successInteractable = miniGame.GetComponent<MiniGame>().GetSuccessInteractable();
                    successInteractable?.Interact();
                    GetComponent<PlayerMovement>().isPlayerMoving = false;
                    GameObject.Find("PlayerCam").GetComponent<PlayerCam>().lockCamera = true;
                    break;
                case "Fail":
                    Interactable failInteractable = miniGame.GetComponent<MiniGame>().GetFailInteractable();
                    failInteractable?.Interact();
                    GetComponent<PlayerMovement>().isPlayerMoving = false;
                    GameObject.Find("PlayerCam").GetComponent<PlayerCam>().lockCamera = true;
                    break;
                case "Partial":
                    Interactable partialInteractable = miniGame.GetComponent<MiniGame>().GetPartialInteractable();
                    partialInteractable?.Interact();
                    GetComponent<PlayerMovement>().isPlayerMoving = false;
                    GameObject.Find("PlayerCam").GetComponent<PlayerCam>().lockCamera = true;
                    break;
            }   
        }
    }

    public Interactable GetInteractable() {
        List<Interactable> interactableList = new List<Interactable>();
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