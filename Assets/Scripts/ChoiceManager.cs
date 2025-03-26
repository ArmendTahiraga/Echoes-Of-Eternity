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
    public string gameResult;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        string choicesFile = Path.Combine(Application.streamingAssetsPath, "Story/GameChoices.json");
        string choicesJson = File.ReadAllText(choicesFile);
        GameChoicesData choicesData = JsonUtility.FromJson<GameChoicesData>(choicesJson);
        gameChoices.Add("graveyard", choicesData.graveyard);
        gameChoices.Add("diner", choicesData.diner);
        gameChoices.Add("warf", choicesData.warf);
        gameChoices.Add("encrypted", choicesData.encrypted);
        gameChoices.Add("bridge", choicesData.bridge);
        gameChoices.Add("final", choicesData.final);
        
        string cluesFile = Path.Combine(Application.streamingAssetsPath, "Story/Clues.json");
        string cluesJson = File.ReadAllText(cluesFile);
        GameCluesData cluesData = JsonUtility.FromJson<GameCluesData>(cluesJson);
        clues = cluesData.clues;
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

    public void CalculateCluePoints() {
        cluePoints = 0;
        
        foreach (ClueData clue in clues) {
            foreach (String clueGathered in cluesGathered) {
                if (clue.name == clueGathered) {
                    cluePoints += clue.value;
                }    
            }
        }
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

    public void ResetCluesGathered() {
        cluesGathered.Clear();
    }

    public void Save(ref ChoiceData choiceData) {
        choiceData.cluesGathered = cluesGathered;
        choiceData.cluePoints = cluePoints;
        choiceData.gameResult = gameResult;
    }

    public void Load(ChoiceData choiceData) {
        cluesGathered = choiceData.cluesGathered;
        cluePoints = choiceData.cluePoints;
        gameResult = choiceData.gameResult;
    }
}