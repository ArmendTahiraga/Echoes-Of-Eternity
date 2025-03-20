using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {
    public static ChoiceManager Instance { get; private set; }
    private Dictionary<string, List<string>> gameChoices = new Dictionary<string, List<string>>();
    private Dictionary<string, Action> choiceActions = new Dictionary<string, Action>();
    private List<string> cluesGathered = new List<string>();
    private List<ClueData> clues = new List<ClueData>();
    private float cluePoints;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        string choicesFile = Path.Combine(Application.dataPath, "Story/GameChoices.json");
        string choicesJson = File.ReadAllText(choicesFile);
        GameChoicesData choicesData = JsonUtility.FromJson<GameChoicesData>(choicesJson);
        gameChoices.Add("graveyard", choicesData.graveyard);
        gameChoices.Add("diner", choicesData.diner);
        gameChoices.Add("warf", choicesData.warf);
        gameChoices.Add("bridge", choicesData.bridge);
        gameChoices.Add("final", choicesData.final);
        
        string cluesFile = Path.Combine(Application.dataPath, "Story/Clues.json");
        string cluesJson = File.ReadAllText(cluesFile);
        GameCluesData cluesData = JsonUtility.FromJson<GameCluesData>(cluesJson);
        clues = cluesData.clues;
        
        cluesGathered.Add("wharfRecord");
        cluesGathered.Add("bridgeFlash");
    }

    public bool IsImportantChoice(string sceneName, string choiceId) {
        return gameChoices.ContainsKey(sceneName) && gameChoices[sceneName].Contains(choiceId);
    }

    public void RegisterChoiceEvent(string choiceId, Action action) {
        choiceActions[choiceId] = action;
    }

    public void TriggerChoice(string choiceId) {
        Action action = choiceActions[choiceId];
        action?.Invoke();
    }

    public void AddClue(string clue) {
        cluesGathered.Add(clue);
    }

    public float CalculateCluePoints() {
        cluePoints = 0;
        
        foreach (ClueData clue in clues) {
            foreach (String clueGathered in cluesGathered) {
                if (clue.name == clueGathered) {
                    cluePoints += clue.value;
                }    
            }
        }
        
        return cluePoints;
    }

    public List<string> GetCluesGathered() {
        return cluesGathered;
    }

    public void RemoveClue(string clue) {
        cluesGathered.Remove(clue);
    }

    public float GetCluePoints() {
        return cluePoints;
    }
}