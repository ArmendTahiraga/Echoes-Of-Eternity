using UnityEngine;

public class GraveInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private AsyncLoader loader;
    [SerializeField] private Objective objective;
    
    public void Interact() {
        if (objective != null) {
            objective.CompleteObjective();
        }
        
        loader.LoadLevel("Scene1");
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}
