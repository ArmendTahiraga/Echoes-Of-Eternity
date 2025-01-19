using DS;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string characterName;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private string[] animationTriggers;
    [SerializeField] private Animator[] animators;
    [SerializeField] private TeleportPlayer teleportPlayer;
    [SerializeField] private Objective objective;
    
    public void Interact() {
        dialogueUI.StartDialogue(characterName, gameObject.GetComponent<DSDialogue>());

        if (objective != null) {
            objective.CompleteObjective();
        }
        
        if (animationTriggers?.Length > 0) {
            for (int i = 0; i < animationTriggers.Length; i++) {
                if (animationTriggers[i] == "PlayerSitTrigger") {
                    teleportPlayer.MovePlayer(new Vector3(-3.632f, 45.9f, 190.45f), Quaternion.Euler(0, 90, 0) , new Vector3(0, -1.971f, 0.019f),
                        new Vector3(0, -0.738f, -0.683f), Quaternion.Euler(0, -265f, 0), Quaternion.Euler(0, 0, 0));
                }

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