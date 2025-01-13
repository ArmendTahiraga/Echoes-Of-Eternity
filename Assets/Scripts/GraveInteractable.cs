using UnityEngine;

public class GraveInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private AsyncLoader loader;
    
    public void Interact() {
        loader.LoadLevel("Graveyard");
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}
