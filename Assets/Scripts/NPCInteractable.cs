using DS;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string characterName;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private string[] animationTriggers;
    [SerializeField] private Animator[] animators;
    [SerializeField] private Objective objective;
    [SerializeField] private bool isOneTime;
    [SerializeField] private bool hasBeenInteracted;

    private void Update() {
        if (isOneTime && hasBeenInteracted) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            this.enabled = false;
        }
    }

    public void Interact() {
        dialogueUI.StartDialogue(characterName, gameObject.GetComponent<DSDialogue>());
        hasBeenInteracted = true;

        if (objective != null) {
            objective.CompleteObjective();
        }
        
        if (animationTriggers?.Length > 0) {
            for (int i = 0; i < animationTriggers.Length; i++) {
                animators[i].SetTrigger(animationTriggers[i]);
            }
        }
    }

    public string GetInteractionText() {
        return interactionText;
    }

    public Transform GetTransform() {
        return transform;
    }
}