using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ObjectiveManager objectiveManager;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private GameObject continueGameButton;
    [SerializeField] private GameObject loadGameButton;
    [SerializeField] private AsyncLoader asyncLoader;
    [SerializeField] private PauseMenuController pauseMenuController;
    private SaveData saveData = new SaveData();

    private void Update() {
        if (continueGameButton) {
            string saveFile = Application.persistentDataPath + "/saveData.json";
            continueGameButton.SetActive(File.Exists(saveFile));
        }
        
        if (loadGameButton) {
            string saveFile = Application.persistentDataPath + "/saveData.json";
            loadGameButton.SetActive(File.Exists(saveFile));
        }
    }

    public void SaveGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        HandleSaveData();
        File.WriteAllText(saveFile, JsonUtility.ToJson(saveData, true));
    }

    private void HandleSaveData() {
        saveData.scene = SceneManager.GetActiveScene().name;
        playerMovement.Save(ref saveData.playerSaveData);
        objectiveManager.Save(ref saveData.objectivesSaveData);
        dialogueUI.Save(ref saveData.dialogueSaveData);
        playerCam.Save(ref saveData.playerCamSaveData);
    }

    public void LoadGame() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        string saveDataContents = File.ReadAllText(saveFile);
        saveData = JsonUtility.FromJson<SaveData>(saveDataContents);
        
        if (SceneManager.GetActiveScene().name != saveData.scene) {
            asyncLoader.LoadLevel(saveData.scene);
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            HandleLoadData();
        }

        if (pauseMenuController != null) {
            pauseMenuController.ResumeGame();
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
        playerMovement = FindObjectOfType<PlayerMovement>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        playerCam = FindObjectOfType<PlayerCam>();
        
        HandleLoadData();
    }

    private void HandleLoadData() {
        playerMovement.Load(saveData.playerSaveData);
        objectiveManager.Load(saveData.objectivesSaveData);
        dialogueUI.Load(saveData.dialogueSaveData);
        playerCam.Load(saveData.playerCamSaveData);
    }

    public void DeleteSaveData() {
        string saveFile = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(saveFile)) {
            File.Delete(saveFile);
        } 
    }
}