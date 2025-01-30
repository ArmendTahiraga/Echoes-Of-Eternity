using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
    [SerializeField] private List<Objective> objectives;
    [SerializeField] private TextMeshProUGUI objectiveText;
    private Objective currentObjective;
    public bool loadGameChanges;

    private void Start() {
        if (loadGameChanges) {
            return;
        }
        
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

    private void Update() {
        if (loadGameChanges) {
            loadGameChanges = false;
        }
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
        objectivesSaveData.currentObjectiveID = currentObjective != null ? currentObjective.uniqueID : "";
    }

    public void Load(ObjectivesSaveData objectivesSaveData) {
        if (currentObjective != null) {
            currentObjective.isActive = false;
        }

        if (objectivesSaveData.currentObjectiveID != "") {
            objectiveText.gameObject.SetActive(true);
            
            Objective[] objectives = FindObjectsOfType<Objective>();
            foreach (Objective objective in objectives) {
                if (objective.uniqueID == objectivesSaveData.currentObjectiveID) {
                    currentObjective = objective;
                }
            }
            
            currentObjective.isActive = true;
            UpdateObjectiveUI();
        } else {
            objectiveText.gameObject.SetActive(false);
        }
    }
}