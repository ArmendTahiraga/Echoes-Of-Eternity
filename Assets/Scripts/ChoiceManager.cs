using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {
    public static ChoiceManager Instance { get; private set; }
    private Dictionary<string, List<string>> gameChoices = new Dictionary<string, List<string>>();
    private Dictionary<string, Action> choiceActions = new Dictionary<string, Action>();
    private List<ClueData> clues = new List<ClueData>();
    private List<string> cluesGathered = new List<string>();
    private float cluePoints;
    public string gameResult;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameChoices.Add("graveyard", new List<string> { "relatives_hint", "game_success", "game_fail" });
        gameChoices.Add("warf", new List<string> {
            "choice_recordConversation", "choice_listenFromDistance", "choice_confrontDrakeAndCrowe",
            "record_success", "record_fail", "listen_success", "confront_success", "confront_escape",
            "confront_dead", "light_detected"
        });
        gameChoices.Add("bridge", new List<string> { "flash_success", "flash_fail", "da_hint" });
        gameChoices.Add("diner", new List<string> { "wharf", "old_town", "bridge" });
        gameChoices.Add("final", new List<string> { "nextEvidence", "success", "fail" });
        gameChoices.Add("encrypted", new List<string> { "encrypted_success", "encrypted_partial", "encrypted_fail" });

        clues = new List<ClueData> {
            new ClueData { name = "graveyardRelatives", value = 0.25f },
            new ClueData { name = "wharfRecord", value = 1f },
            new ClueData { name = "wharfListen", value = 0.5f },
            new ClueData { name = "wharfConfront", value = 0.75f },
            new ClueData { name = "encryptedFull", value = 1f },
            new ClueData { name = "encryptedHalf", value = 0.5f },
            new ClueData { name = "bridgeFlash", value = 1f },
            new ClueData { name = "bridgeDa", value = 0.5f }
        };
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