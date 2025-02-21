using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour {
    [SerializeField] private GameObject container;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private GameObject miniGame;

    private void Update() {
        Interactable interactable = playerInteract.GetInteractable();
        
        if (interactable != null && !dialogueUI.GetHasDialogueStarted()) {
            Show(interactable);
        } else {
            Hide();
        }

        if (interactable != null && miniGame != null) {
            if (!miniGame.GetComponent<MiniGame>().GetHasMiniGameStarted()) {
                Show(interactable);
            } else {
                Hide();
            }
        }
    }
    
    private void Show(Interactable interactable) {
        container.SetActive(true);
        interactionText.text = interactable.GetInteractionText();
    }
    
    private void Hide() {
        container.SetActive(false);
    }
}