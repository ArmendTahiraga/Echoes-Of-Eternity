using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
    [SerializeField] private List<Objective> objectives;
    [SerializeField] private TextMeshProUGUI objectiveText;
    private Objective currentObjective;    

    private void Start() {
        if (objectives.Count == 0) {
            objectiveText.gameObject.SetActive(false);
            return;
        }
        
        foreach (Objective objective in objectives) {
            objective.onObjectiveComplete.AddListener(() => OnObjectiveComplete(objective));
        }
        
        currentObjective = objectives[0];
        currentObjective.isActive = true;
        UpdateObjectiveUI();
    }

    private void OnObjectiveComplete(Objective objective) {
        int objectiveIndex = objectives.IndexOf(objective);
        if (objectiveIndex + 1 < objectives.Count) {
            currentObjective = objectives[objectiveIndex + 1];
            currentObjective.isActive = true;
            objective.isActive = false;
            UpdateObjectiveUI();
        } else {
            objective.isActive = false;
            CompletedObjectives();
        }
        
    }

    private void UpdateObjectiveUI() {
        objectiveText.text = currentObjective.GetObjective();
    }

    private void CompletedObjectives() {
        objectiveText.gameObject.SetActive(false);
        currentObjective = null;
    }

    public void Save(ref ObjectivesSaveData objectivesSaveData) {
        objectivesSaveData.currentObjective = currentObjective;
    }

    public void Load(ObjectivesSaveData objectivesSaveData) {
        if (currentObjective != null) {
            currentObjective.isActive = false;
        }

        if (objectivesSaveData.currentObjective != null) {
            objectiveText.gameObject.SetActive(true);
            currentObjective = objectivesSaveData.currentObjective;
            currentObjective.isActive = true;
            UpdateObjectiveUI();
        } else {
            objectiveText.gameObject.SetActive(false);
        }
    }
}