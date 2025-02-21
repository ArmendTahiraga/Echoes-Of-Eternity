using UnityEngine;

public class MiniGameInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private Objective objective;
    [SerializeField] private GameObject miniGame;
    
    public void Interact() {
        if (objective != null) {
            objective.CompleteObjective();
        }
        
        miniGame.GetComponent<MiniGame>().StartGame();
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}