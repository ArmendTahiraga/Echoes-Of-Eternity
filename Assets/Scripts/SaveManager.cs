using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PauseMenuController pauseMenuController;
    private SaveData saveData = new SaveData();
    
    public void SaveGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        HandleSaveData();
        File.WriteAllText(saveFile, JsonUtility.ToJson(saveData));
    }

    private void HandleSaveData() {
        playerMovement.Save(ref saveData.playerSaveData);
    }

    public void LoadGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        string saveDataContents = File.ReadAllText(saveFile);
        saveData = JsonUtility.FromJson<SaveData>(saveDataContents);
        HandleLoadData();
    }

    private void HandleLoadData() {
        playerMovement.Load(saveData.playerSaveData);
    }
}