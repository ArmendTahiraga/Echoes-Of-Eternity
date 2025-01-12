using UnityEngine;

public interface Interactable {
    void Interact();
    string GetInteractionText();
    Transform GetTransform();
}