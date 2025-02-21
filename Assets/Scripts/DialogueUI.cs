using System.Collections.Generic;
using DS;
using DS.Data;
using DS.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour {
    [SerializeField] private List<Button> choicesButtons;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject miniGame;
    private DSDialogue dialogue;
    private bool dialogueActive;
    
    private void Update() {
        if (dialogueActive) {
            Show();
            CheckDialogueInput();
        } else {
            if (miniGame != null && miniGame.GetComponent<MiniGame>().GetHasMiniGameStarted()) {
                Hide();
                return;
            }
            
            GameObject.Find("Player").GetComponent<PlayerMovement>().isPlayerMoving = true;
            GameObject.Find("PlayerCam").GetComponent<PlayerCam>().lockCamera = false;
            Hide();
        }
    }
    
    private void Show() {
        panel.gameObject.SetActive(true);
    }
    
    private void Hide() {
        panel.gameObject.SetActive(false);
    }

    public void StartDialogue(string characterName, DSDialogue dialogue) {
        dialogueActive = true;
        this.dialogue = dialogue;
        this.characterName.text = characterName.Replace("0", " ");
        DSDialogueSO currentDialogue = dialogue.GetDialogue();
        dialogueText.text = currentDialogue.dialogueText;
        DisplayChoices(currentDialogue);
    }

    public bool GetHasDialogueStarted() {
        return dialogueActive;
    }
    
    private void CheckDialogueInput() {
        for (int i = 0; i < dialogue.GetDialogue().choices.Count; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !PauseMenuController.isGamePaused) {
                OnChoiceSelected(i);
                return;
            }
        }
    }
    
    private void OnChoiceSelected(int choiceIndex) {
        DSDialogueChoiceData selectedChoice = dialogue.GetDialogue().choices[choiceIndex];
        DSDialogueSO nextDialogue = selectedChoice.nextDialogue;
        DSDialogueContainerSO dialogueContainer = dialogue.GetDialogueContainer();
        List<DSDialogueGroupSO> groups = dialogueContainer.GetGroups();

        
        string[] selectedChoiceParts = selectedChoice.text.Split('\\');
        
        if (selectedChoiceParts.Length == 3) {
            nextDialogue = null;
            if (ChoiceManager.Instance.IsImportantChoice(selectedChoiceParts[1], selectedChoiceParts[2])) {
                ChoiceManager.Instance.TriggerChoice(selectedChoiceParts[2]);
            }
        }
        
        if (nextDialogue == null) {
            dialogueActive = false;

            foreach (DSDialogueGroupSO group in groups) {
                List<DSDialogueSO> dialogues = dialogueContainer.GetGroupDialogues(group);
                foreach (DSDialogueSO dialogue in dialogues) {
                    if (dialogue.isStartingDialogue) {
                        this.dialogue.SetDSDialogueSO(dialogue);
                        this.dialogue.SetDSDialogueGroupSO(group);
                        return;
                    }
                }
            }
        }

        foreach (DSDialogueGroupSO group in groups) {
            List<DSDialogueSO> dialogues = dialogueContainer.GetGroupDialogues(group);
            if (dialogues.Contains(nextDialogue)) {
                dialogue.SetDSDialogueSO(nextDialogue);
                dialogue.SetDSDialogueGroupSO(group);
                StartDialogue(group.groupName, dialogue);
                return;
            }
        }
    }

    public void Save(ref DialogueSaveData dialogueSaveData) {
        dialogueSaveData.dialogueActive = dialogueActive;
        dialogueSaveData.dialogueID = dialogue ? dialogue.uniqueID: null;
        dialogueSaveData.dialogueSOID = dialogue ? dialogue.GetDialogue().uniqueID : null;
        dialogueSaveData.dialogueGroupSOID = dialogue ? dialogue.GetDialogueGroup().uniqueID : null;
        dialogueSaveData.characterName = characterName.text;
        dialogueSaveData.dialogueText = dialogueText.text;
    }

    public void Load(DialogueSaveData dialogueSaveData) {
        dialogueActive = dialogueSaveData.dialogueActive;
        DSDialogueGroupSO dialogueGroup = null;
        
        if (!string.IsNullOrEmpty(dialogueSaveData.dialogueID)) {
            DSDialogue[] dialogues = FindObjectsOfType<DSDialogue>();
            foreach (DSDialogue dialogue in dialogues) {
                if (dialogue.uniqueID== dialogueSaveData.dialogueID) {
                    this.dialogue = dialogue;
                }
            }

            List<DSDialogueGroupSO> dialogueGroupSos = this.dialogue.GetDialogueContainer().GetGroups();
            foreach (DSDialogueGroupSO dialogueGroupSo in dialogueGroupSos) {
                if (dialogueGroupSo.uniqueID == dialogueSaveData.dialogueGroupSOID) {
                    dialogue.SetDSDialogueGroupSO(dialogueGroupSo);
                    dialogueGroup = dialogueGroupSo;
                }
            }
            
            List<DSDialogueSO> dialogueSos = dialogue.GetDialogueContainer().GetGroupDialogues(dialogueGroup);
            foreach (DSDialogueSO dialogueSo in dialogueSos) {
                if (dialogueSo.uniqueID == dialogueSaveData.dialogueSOID) {
                    dialogue.SetDSDialogueSO(dialogueSo);
                }
            }
            
            DisplayChoices(this.dialogue.GetDialogue());
        }
        
        characterName.text = dialogueSaveData.characterName;
        dialogueText.text = dialogueSaveData.dialogueText;
    }

    private void DisplayChoices(DSDialogueSO dialogue) {
        for (int i = 0; i < choicesButtons.Count; i++) {
            if (i < dialogue.choices.Count) {
                choicesButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI choiceText = choicesButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = (i + 1) + ".  " + dialogue.choices[i].text.Split("\\")[0];
            } else {
                choicesButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
