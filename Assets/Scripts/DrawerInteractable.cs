using UnityEngine;

public class DrawerInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private GameObject memoryGameCanvas; // Reference to Memory Game UI
    [SerializeField] private Objective objective;
    
    public void Interact() {
        if (objective != null) {
            objective.CompleteObjective();
        }

        if (memoryGameCanvas != null) {
            memoryGameCanvas.SetActive(true); // Show the memory game UI
        }
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}