using System.Collections;
using DS;
using UnityEngine;

public class MonologueInteractable : MonoBehaviour {
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private string characterName;
    [SerializeField] private bool isOneTime;
    [SerializeField] private bool hasBeenPlayed;
    [SerializeField] private float delay;
    public static bool loadGameChanges;

    private void Update() {
        loadGameChanges = false;
    }

    private IEnumerator PlayMonologue() {
        yield return new WaitForSeconds(delay);
        Interact();
    }
    
    private void Interact() {
        playerMovement.isPlayerMoving = false;
        playerCam.lockCamera = true;
        dialogueUI.StartDialogue(characterName, gameObject.GetComponent<DSDialogue>());
        hasBeenPlayed = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (isOneTime && !hasBeenPlayed && !loadGameChanges) {
                StartCoroutine(PlayMonologue());
            }
        }
    }
}