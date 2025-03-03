using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordMiniGame : MonoBehaviour, MiniGame {
    [SerializeField] private Image keyImage;
    [SerializeField] private Image timerImage;
    [SerializeField] private int keysToPress = 4;
    [SerializeField] private float keyPressTime = 2f;
    [SerializeField] private Sprite[] keySprites;
    [SerializeField] private NPCInteractable successInteractable;
    [SerializeField] private NPCInteractable failInteractable;
    [SerializeField] private GameObject miniGameStarter;
    private List<KeyCode> possibleKeys = new List<KeyCode>();
    private Dictionary<KeyCode, Sprite> keySpriteMap = new Dictionary<KeyCode, Sprite>();
    private KeyCode currentKey;
    private float currentTimer;
    private int keysPressed;
    private bool isRecording;
    private bool canPressKey = true;
    private bool changeTimerFill = true;
    private Color timerColor;
    public bool hasMiniGameStarted;
    public bool isCursorNeeded;
    public string miniGameResult = "";

    private void Start() {
        InitializeKeyLists();
        timerColor = timerImage.color;
    }

    private void Update() {

        if (isRecording) {
            if (changeTimerFill) {
                currentTimer -= Time.deltaTime;
                timerImage.fillAmount = currentTimer / keyPressTime;
            }

            if (currentTimer <= 0) {
                FailMiniGame();
            }

            if (Input.GetKeyUp(KeyCode.E)) {
                FailMiniGame();
            }

            if (Input.anyKeyDown && canPressKey) {
                foreach (KeyCode key in possibleKeys) {
                    if (Input.GetKeyDown(key)) {
                        StartCoroutine(key == currentKey ? CorrectKeyPressed() : WrongKeyPressed());
                        break;
                    }
                }
            }
        }
    }

    public void StartGame() {
        hasMiniGameStarted = true;
        isRecording = true;
        keysPressed = 0;
        keyImage.gameObject.SetActive(true);
        timerImage.gameObject.SetActive(true);
        ShowNextKey();
    }
    
    public bool GetHasMiniGameStarted() {
        return hasMiniGameStarted;
    }

    public bool GetIsCursorNeeded() {
        return isCursorNeeded;
    }

    public string GetMiniGameResult() {
        return miniGameResult;
    }
    
    public NPCInteractable GetSuccessInteractable() {
        return successInteractable;
    }
    
    public NPCInteractable GetFailInteractable() {
        return failInteractable;
    }
    
    private void ShowNextKey() {
        if (keysPressed >= keysToPress) {
            SuccessMiniGame();
            return;
        }
        
        float randomX = Random.Range(-300f, 300f);
        float randomY = Random.Range(-200f, 200f);
        keyImage.transform.localPosition = new Vector3(randomX, randomY, keyImage.transform.localPosition.z);
        timerImage.transform.localPosition = new Vector3(randomX, randomY, timerImage.transform.localPosition.z);
        
        int keyIndex = Random.Range(0, possibleKeys.Count);
        currentKey = possibleKeys[keyIndex];
        keyImage.sprite = keySpriteMap[currentKey];
        currentTimer = keyPressTime;
        timerImage.fillAmount = 1;
        canPressKey = true;
        keysPressed++;
    }

    private IEnumerator CorrectKeyPressed() {
        canPressKey = false;
        changeTimerFill = false;
        timerImage.color = Color.green;
        timerImage.fillAmount = 1;
        yield return new WaitForSeconds(0.5f);
        
        timerImage.color = timerColor;
        changeTimerFill = true;
        ShowNextKey();
    }

    private IEnumerator WrongKeyPressed() {
        canPressKey = false;
        changeTimerFill = false;
        timerImage.color = Color.red;
        timerImage.fillAmount = 1;
        yield return new WaitForSeconds(0.5f);
        
        timerImage.color = timerColor;
        changeTimerFill = true;
        FailMiniGame();
    }

    private void FailMiniGame() {
        hasMiniGameStarted = false;
        isRecording = false;
        
        miniGameResult = "Fail";
        StartCoroutine(RemoveMiniGameResultAndMiniGameStarter());
        
        keyImage.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(false);
    }

    private void SuccessMiniGame() {
        hasMiniGameStarted = false;
        isRecording = false;
        
        miniGameResult = "Success";
        StartCoroutine(RemoveMiniGameResultAndMiniGameStarter());
        
        keyImage.gameObject.SetActive(false);
        timerImage.gameObject.SetActive(false);
    }

    private IEnumerator RemoveMiniGameResultAndMiniGameStarter() {
        yield return new WaitForSeconds(0.1f);
        miniGameResult = "";
        miniGameStarter.SetActive(false);
    }
    
    private void InitializeKeyLists() {
        for (int i = (int) KeyCode.A; i <= (int) KeyCode.Z; i++) {
            KeyCode key = (KeyCode) i;
            if (key != KeyCode.E) {
                possibleKeys.Add(key);
            }
        }

        for (int i = 0; i < possibleKeys.Count && i < keySprites.Length; i++) {
            keySpriteMap[possibleKeys[i]] = keySprites[i];
        }
    }
}