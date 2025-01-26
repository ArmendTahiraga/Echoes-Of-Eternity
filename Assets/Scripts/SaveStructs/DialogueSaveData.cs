using DS;
using DS.ScriptableObjects;

[System.Serializable]
public struct DialogueSaveData {
    public bool dialogueActive;
    public DSDialogue dialogue;
    public DSDialogueSO dialogueSO;
    public DSDialogueGroupSO dialogueGroupSO;
    public string characterName;
    public string dialogueText;
}