using DS;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string characterName;
    [SerializeField] private DialogueUI dialogueUI;

    public void Interact() {
        dialogueUI.StartDialogue(characterName, gameObject.GetComponent<DSDialogue>());
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}