using System.Collections;
using DS;
using UnityEngine;

public class LightDetectionInteractable : MonoBehaviour {
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private string characterName;
    private static bool hasWarningBeenPlayed;
    
    private IEnumerator PlayMonologue() {
        yield return new WaitForSeconds(0.01f);
        Interact();
    }
    
    private void Interact() {
        playerMovement.isPlayerMoving = false;
        playerCam.lockCamera = true;
        
        if (!hasWarningBeenPlayed) {
            DSDialogue[] dsDialogues = gameObject.GetComponents<DSDialogue>();
            foreach (DSDialogue dsDialogue in dsDialogues) {
                if (dsDialogue.GetDialogue().dialogueName.Contains("Warning")) {
                    dialogueUI.StartDialogue(characterName, dsDialogue);
                    hasWarningBeenPlayed = true;
                }
            }
        } else {
            DSDialogue[] dsDialogues = gameObject.GetComponents<DSDialogue>();
            foreach (DSDialogue dsDialogue in dsDialogues) {
                if (dsDialogue.GetDialogue().dialogueName.Contains("Detected")) {
                    dialogueUI.StartDialogue(characterName, dsDialogue);
                    hasWarningBeenPlayed = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(PlayMonologue());
        }
    }
}