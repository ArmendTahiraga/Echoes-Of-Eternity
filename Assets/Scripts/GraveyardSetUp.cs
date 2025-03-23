using System.Collections.Generic;
using UnityEngine;

public class GraveyardSetUp : MonoBehaviour {
    [SerializeField] private GameObject relative1;
    [SerializeField] private GameObject relative2;
    [SerializeField] private GameObject margaret;
    [SerializeField] private GameObject monologueStarterSuccess;
    [SerializeField] private GameObject monologueStarterFail;
    [SerializeField] private ObjectiveManager objectiveManager;

    private void Start() {
        if (ChoiceManager.Instance.gameResult == "Success") {
            margaret.SetActive(false);
            relative1.SetActive(false);
            relative2.SetActive(false);
            monologueStarterSuccess.SetActive(true);
            ChangeObjectives();
        } else if (ChoiceManager.Instance.gameResult == "Fail") {
            margaret.SetActive(true);
            relative1.SetActive(false);
            relative2.SetActive(false);
            monologueStarterFail.SetActive(true);
            ChangeObjectives();
        } else {
            margaret.SetActive(false);
        }
    }
    
    private void ChangeObjectives() {
        List<Objective> objectivesList = new List<Objective>();

        foreach (Objective objective in objectiveManager.objectives) {
            if (objective.objectiveChoice == "epilogue") {
                objectivesList.Add(objective);
            }
        }

        objectiveManager.objectives = objectivesList;
        objectiveManager.currentObjective = objectivesList[0];
        objectiveManager.currentObjective.isActive = true;
        objectiveManager.UpdateObjectiveUI();
    }
}