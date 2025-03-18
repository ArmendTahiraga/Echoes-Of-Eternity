using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceEventManager : MonoBehaviour {
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private AsyncLoader asyncLoader;
    [SerializeField] private Animator deathScreenCanvasAnimator;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private CapsuleCollider[] lightColliders;

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
        ChoiceManager.Instance.AddClue("wharf_record");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfRecordFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfListenSuccess() {
        ChoiceManager.Instance.AddClue("wharf_listen");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontSuccess() {
        ChoiceManager.Instance.AddClue("wharf_confront");
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
        ChoiceManager.Instance.AddClue("bridge_flash");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeFlashFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeDaHint() {
        ChoiceManager.Instance.AddClue("bridge_da");
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
}