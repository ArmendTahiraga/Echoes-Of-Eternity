using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PlayerCam playerCam;
    private SaveData saveData = new SaveData();
    
    public void SaveGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        HandleSaveData();
        File.WriteAllText(saveFile, JsonUtility.ToJson(saveData, true));
    }

    private void HandleSaveData() {
        playerMovement.Save(ref saveData.playerSaveData);
        objectiveManager.Save(ref saveData.objectivesSaveData);
        dialogueUI.Save(ref saveData.dialogueSaveData);
        playerCam.Save(ref saveData.playerCamSaveData);
    }

    public void LoadGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        string saveDataContents = File.ReadAllText(saveFile);
        saveData = JsonUtility.FromJson<SaveData>(saveDataContents);
        HandleLoadData();
    }

    private void HandleLoadData() {
        playerMovement.Load(saveData.playerSaveData);
        objectiveManager.Load(saveData.objectivesSaveData);
        dialogueUI.Load(saveData.dialogueSaveData);
        playerCam.Load(saveData.playerCamSaveData);
    }       
}