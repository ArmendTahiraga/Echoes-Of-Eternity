using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceEventManager : MonoBehaviour {
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private AsyncLoader asyncLoader;
    [SerializeField] private Animator deathScreenCanvasAnimator;

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
        ChangeObjectives("choice_confrontDrakeAndCrowe");
        DestroyChoiceSpecificObjects("choice_confrontDrakeAndCrowe");
    }

    private IEnumerator ChangeScene(string scene, float delay) {
        yield return new WaitForSeconds(delay);
        asyncLoader.LoadLevel(scene);
    }

    private void WarfRecordSuccess() {
        ChoiceManager.Instance.AddClue("record_success");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfRecordFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfListenSuccess() {
        ChoiceManager.Instance.AddClue("listen_success");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontSuccess() {
        ChoiceManager.Instance.AddClue("confront_success");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontEscape() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void WarfConfrontDead() {
        deathScreenCanvasAnimator.SetTrigger("ShowDeathScreen");
        StartCoroutine(ChangeScene("DarkStorm", 1f));
    }

    private void WarfLightDetected() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }

    private void BridgeFlashSuccess() {
        ChoiceManager.Instance.AddClue("flash_success");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeFlashFail() {
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
    
    private void BridgeDaHint() {
        ChoiceManager.Instance.AddClue("da_hint");
        StartCoroutine(ChangeScene("Final Confrontation", 1f));
    }
}