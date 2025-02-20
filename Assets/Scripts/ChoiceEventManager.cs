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
        
        foreach(Objective objective in objectiveManager.objectives){
            if (objective.objectiveChoice == choiceId) {
                objectivesList.Add(objective);
            }
        }
        
        objectiveManager.objectives = objectivesList;
        objectiveManager.currentObjective = objectivesList[0];
        objectiveManager.UpdateObjectiveUI();
    }

    private void WarfRecordConversation() {
        ChangeObjectives("choice_recordConversation");
    }

    private void WarfListenFromDistance() {
        ChangeObjectives("choice_listenFromDistance");
    }

    private void WarfConfrontDrakeAndCrowe() {
        ChangeObjectives("choice_confrontDrakeAndCrowe");
    }
}