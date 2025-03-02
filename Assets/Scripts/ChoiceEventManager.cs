using System.Collections.Generic;
using UnityEngine;

public class ChoiceEventManager : MonoBehaviour {
    [SerializeField] private ObjectiveManager objectiveManager;

    private void Start() {
        ChoiceManager.Instance.RegisterChoiceEvent("choice_recordConversation", WarfRecordConversation);
        ChoiceManager.Instance.RegisterChoiceEvent("choice_listenFromDistance", WarfListenFromDistance);
        ChoiceManager.Instance.RegisterChoiceEvent("choice_confrontDrakeAndCrowe", WarfConfrontDrakeAndCrowe);
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
}