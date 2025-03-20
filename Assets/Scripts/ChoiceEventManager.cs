using System.Collections;
using System.Collections.Generic;
using DS;
using DS.ScriptableObjects;
using UnityEngine;

public class ChoiceEventManager : MonoBehaviour {
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private AsyncLoader asyncLoader;
    [SerializeField] private Animator deathScreenCanvasAnimator;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private CapsuleCollider[] lightColliders;
    [SerializeField] private List<DSDialogueContainerSO> evidenceDialogues;
    [SerializeField] private DSDialogue finalDialogueComponent;
    [SerializeField] private DSDialogueContainerSO finalDialogueEndSuccess;
    [SerializeField] private DSDialogueContainerSO finalDialogueEndFail;
    [SerializeField] private DialogueUI dialogueUI;

    private void Start() {
        ChoiceManager.Instance.RegisterChoiceEvent("choice_recordConversation", WarfRecordConversation);
        ChoiceManager.Instance.RegisterChoiceEvent("choice_listenFromDistance", WarfListenFromDistance);
        ChoiceManager.Instance.RegisterChoiceEvent("choice_confrontDrakeAndCrowe", WarfConfrontDrakeAndCrowe);
        ChoiceManager.Instance.RegisterChoiceEvent("record_success", WarfRecordSuccess);
        ChoiceManager.Instance.RegisterChoiceEvent("record_fail", WarfRecordFail);
        ChoiceManager.Instance.RegisterChoiceEvent("listen_success", WarfListenSuccess);
        ChoiceManager.Instance.RegisterChoiceEvent("confront_success", WarfConfrontSuccess);
        ChoiceManager.Instance.RegisterChoiceEvent("confront_escape", WarfConfrontEscape);
        ChoiceManager.Instance.RegisterChoiceEvent("confront_dead", WarfConfrontDead);
        ChoiceManager.Instance.RegisterChoiceEvent("light_detected", WarfLightDetected);
        ChoiceManager.Instance.RegisterChoiceEvent("flash_success", BridgeFlashSuccess);
        ChoiceManager.Instance.RegisterChoiceEvent("flash_fail", BridgeFlashFail);
        ChoiceManager.Instance.RegisterChoiceEvent("da_hint", BridgeDaHint);
        ChoiceManager.Instance.RegisterChoiceEvent("wharf", DinerGoToWharf);
        ChoiceManager.Instance.RegisterChoiceEvent("old_town", DinerGoToOldTown);
        ChoiceManager.Instance.RegisterChoiceEvent("bridge", DinerGoToBridge);
        ChoiceManager.Instance.RegisterChoiceEvent("nextEvidence", FinalNextEvidence);
        ChoiceManager.Instance.RegisterChoiceEvent("success", FinalSuccess);
        ChoiceManager.Instance.RegisterChoiceEvent("fail", FinalFail);
        ChoiceManager.Instance.RegisterChoiceEvent("relatives_hint", GraveyardRelativesHint);
    }

    private void ChangeObjectives(string choiceId) {
        List<Objective> objectivesList = new List<Objective>();

        foreach (Objective objective in objectiveManager.objectives) {
            if (objective.objectiveChoice == choiceId) {
                objectivesList.Add(objective);
            }
        }

        objectiveManager.objectives = objectivesList;
        objectiveManager.currentObjective = objectivesList[0];
        objectiveManager.currentObjective.isActive = true;
        objectiveManager.UpdateObjectiveUI();
    }

    private void DestroyChoiceSpecificObjects(string choiceId) {
        GameObject[] sceneObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject sceneObject in sceneObjects) {
            if (sceneObject.tag.Contains("Choice-")) {
                if (sceneObject.CompareTag("Choice-RecordConversation")) {
                    if (choiceId != "choice_recordConversation") {
                        Destroy(sceneObject);
                    }
                } else if (sceneObject.CompareTag("Choice-ListenFromDistance")) {
                    if (choiceId != "choice_listenFromDistance") {
                        Destroy(sceneObject);
                    }
                } else if (sceneObject.CompareTag("Choice-ConfrontDrakeAndCrowe")) {
                    if (choiceId != "choice_confrontDrakeAndCrowe") {
                        Destroy(sceneObject);
                    }
                }
            }
        }
    }

    private void WarfRecordConversation() {
        ChangeObjectives("choice_recordConversation");
        DestroyChoiceSpecificObjects("choice_recordConversation");
    }

    private void WarfListenFromDistance() {
        ChangeObjectives("choice_listenFromDistance");
        DestroyChoiceSpecificObjects("choice_listenFromDistance");
    }

    private void WarfConfrontDrakeAndCrowe() {
        for (int i = 0; i < lightColliders.Length; i++) {
            lightColliders[i].enabled = false;
        }
        
        ChangeObjectives("choice_confrontDrakeAndCrowe");
        DestroyChoiceSpecificObjects("choice_confrontDrakeAndCrowe");
    }

    private IEnumerator ChangeScene(string scene, float delay) {
        yield return new WaitForSeconds(delay);
        asyncLoader.LoadLevel(scene);
    }

    private void WarfRecordSuccess() {
        ChoiceManager.Instance.AddClue("wharfRecord");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfRecordFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfListenSuccess() {
        ChoiceManager.Instance.AddClue("wharfListen");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontSuccess() {
        ChoiceManager.Instance.AddClue("wharfConfront");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontEscape() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontDead() {
        gameUI.SetActive(false);
        deathScreenCanvasAnimator.SetTrigger("ShowDeathScreen");
        StartCoroutine(ChangeScene("DarkStorm", 1f));
    }

    private void WarfLightDetected() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void BridgeFlashSuccess() {
        ChoiceManager.Instance.AddClue("bridgeFlash");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeFlashFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeDaHint() {
        ChoiceManager.Instance.AddClue("bridgeDa");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void DinerGoToWharf() {
        StartCoroutine(ChangeScene("Wharf at Midnight - Optional Scene", 1f));
    }
    
    private void DinerGoToOldTown() {
        StartCoroutine(ChangeScene("Encrypted Records", 1f));
    }
    
    private void DinerGoToBridge() {
        StartCoroutine(ChangeScene("Scene2", 1f));
    }

    private void FinalNextEvidence() {
        List<string> cluesGathered = ChoiceManager.Instance.GetCluesGathered();
        string groupName;
        
        foreach (string clue in cluesGathered) {
            if (clue != "graveyardRelatives") {
                foreach (DSDialogueContainerSO evidenceDialogue in evidenceDialogues) {
                    if (evidenceDialogue.fileName == clue) {
                        groupName = ChangeDialogue(evidenceDialogue);
                        dialogueUI.StartDialogue(groupName, finalDialogueComponent);
                        ChoiceManager.Instance.RemoveClue(clue);
                        return;
                    }
                }
            }
        }

        if (ChoiceManager.Instance.GetCluePoints() >= 2) {
            groupName = ChangeDialogue(finalDialogueEndSuccess);
        } else {
            groupName = ChangeDialogue(finalDialogueEndFail);
        }
        
        dialogueUI.StartDialogue(groupName, finalDialogueComponent);
    }

    private string ChangeDialogue(DSDialogueContainerSO dialogueContainerSo) {
        finalDialogueComponent.SetDSDialogueContainerSO(dialogueContainerSo);
        foreach (DSDialogueGroupSO dialogueGroupSo in dialogueContainerSo.GetGroups()) {
            foreach (DSDialogueSO dialogueSo in dialogueContainerSo.GetGroupDialogues(dialogueGroupSo)) {
                if (dialogueSo.isStartingDialogue) {
                    finalDialogueComponent.SetDSDialogueGroupSO(dialogueGroupSo);
                    finalDialogueComponent.SetDSDialogueSO(dialogueSo);
                    return dialogueGroupSo.groupName;
                }
            }
        }
        
        return null;
    }

    private void GraveyardRelativesHint() {
        ChoiceManager.Instance.AddClue("graveyardRelatives");
    }

    private void FinalSuccess() {
        StartCoroutine(ChangeScene("DarkStorm", 1f));
    }

    private void FinalFail() {
        StartCoroutine(ChangeScene("DarkStorm", 1f));
    }
}