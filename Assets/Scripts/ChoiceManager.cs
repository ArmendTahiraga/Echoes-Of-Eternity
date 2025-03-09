using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {
    public static ChoiceManager Instance { get; private set; }
    private Dictionary<string, List<string>> gameChoices = new Dictionary<string, List<string>>();
    private Dictionary<string, Action> choiceActions = new Dictionary<string, Action>();
    private List<string> cluesGathered = new List<string>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        string file = Path.Combine(Application.dataPath, "Story/GameChoices.json");
        string jsonText = File.ReadAllText(file);
        GameChoicesData choicesData = JsonUtility.FromJson<GameChoicesData>(jsonText);
        gameChoices = new Dictionary<string, List<string>>();
        gameChoices.Add("warf", choicesData.warf);
        gameChoices.Add("bridge", choicesData.bridge);
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
}