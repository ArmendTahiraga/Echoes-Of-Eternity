using DS;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, Interactable {
    [SerializeField] private string interactionText;
    [SerializeField] private string characterName;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private string[] animationTriggers;
    [SerializeField] private Animator[] animators;

    public void Interact() {
        dialogueUI.StartDialogue(characterName, gameObject.GetComponent<DSDialogue>());

        if (animationTriggers.Length > 0) {
            for (int i = 0; i < animationTriggers.Length; i++) {
                if (animationTriggers[i] == "PlayerSitTrigger") {
                    GameObject.Find("Player").GetComponent<Transform>().position = new Vector3(-3.632f, 45.872f, 190.45f);
                    GameObject.Find("PlayerObject").GetComponent<Transform>().localPosition = new Vector3(0, -1.971f, 0.019f);
                    GameObject.Find("CameraPosition").GetComponent<Transform>().localPosition = new Vector3(0, -0.738f, -0.683f);
                    GameObject.Find("PlayerCam").GetComponent<Transform>().rotation = Quaternion.Euler(0, -175f, 0);
                    GameObject.Find("Orientation").GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
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