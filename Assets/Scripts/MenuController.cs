using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    private static readonly int Animate = Animator.StringToHash("Animate");
    private Animator cameraAnimator;
    public GameObject firstMenu;
    public GameObject playMenu;
    public GameObject exitMenu;
    public enum Theme {
        custom1,
        custom2,
        custom3
    };
    public Theme theme;
    public ThemedUIData themeController;
    public GameObject controlsPanel;
    public GameObject audioPanel;
    public GameObject audioLine;
    public GameObject controlsLine;
    public GameObject gameVolumeSlider;
    public GameObject soundEffectsVolumeSlider;
    public GameObject cameraSensitivitySlider;
    public AudioSource hoverSound;

    private void Awake() {
        LoadThemeColor();
    }

    private void Start() {
        cameraAnimator = transform.GetComponent<Animator>();
        gameVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("GameVolume", 1);
        soundEffectsVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1);
        cameraSensitivitySlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("CameraSensitivity", 0.5f);

        playMenu.SetActive(false);
        exitMenu.SetActive(false);
        firstMenu.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        themeController.textColor = themeController.custom1.text1;
    }

    public void PlayCampaign() {
        exitMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void ReturnMenu() {
        playMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    public void GoToSettings() {
        playMenu.SetActive(false);
        cameraAnimator.SetFloat(Animate, 1);
    }

    public void GoToMainPage() {
        cameraAnimator.SetFloat(Animate, 0);
    }

    private void DisablePanels() {
        controlsPanel.SetActive(false);
        audioPanel.SetActive(false);

        audioLine.SetActive(false);
        controlsLine.SetActive(false);
    }

    public void AudioPanel() {
        DisablePanels();
        audioPanel.SetActive(true);
        audioLine.SetActive(true);
    }

    public void ControlsPanel() {
        DisablePanels();
        controlsPanel.SetActive(true);
        controlsLine.SetActive(true);
    }

    public void PlayHover() {
        hoverSound.Play();
    }

    public void AreYouSure() {
        exitMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
				Application.Quit();
        #endif
    }

    public void GameVolumeSlider() {
        PlayerPrefs.SetFloat("GameVolume", gameVolumeSlider.GetComponent<Slider>().value);
    }
    
    public void SoundEffectsSlider() {
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolumeSlider.GetComponent<Slider>().value);
    }
    
    public void CameraSensitivitySlider() {
        PlayerPrefs.SetFloat("CameraSensitivity", cameraSensitivitySlider.GetComponent<Slider>().value);
    }

    public void ThemeClickPurple() {
        Color color = new Color(84f / 255f, 64f / 255f, 224f / 255f, 1f);
        themeController.currentColor = color;
        SaveThemeColor(color);
    }

    public void ThemeClickRed() {
        Color color = new Color(255f / 255f, 52f / 255f, 52f / 255f, 1f);
        themeController.currentColor = color;
        SaveThemeColor(color);
    }

    public void ThemeClickGreen() {
        Color color = new Color(68f / 255f, 255f / 255f, 0f / 255f, 1f);
        themeController.currentColor = color;
        SaveThemeColor(color);
    }

    public void ThemeClickBlue() {
        Color color = new Color(0f / 255f, 143f / 255f, 255f / 255f, 1f);
        themeController.currentColor = color;
        SaveThemeColor(color);
    }

    public void ThemeClickYellow() {
        Color color = new Color(255f / 255f, 175f / 255f, 0f / 255f, 1f);
        themeController.currentColor = color;
        SaveThemeColor(color);
    }
    
    private void SaveThemeColor(Color color) {
        PlayerPrefs.SetFloat("Theme-R", color.r);
        PlayerPrefs.SetFloat("Theme-G", color.g);
        PlayerPrefs.SetFloat("Theme-B", color.b);
        PlayerPrefs.SetFloat("Theme-A", color.a);
        PlayerPrefs.Save();
    }

    private void LoadThemeColor() {
        if (PlayerPrefs.HasKey("Theme-R")) {
            float r = PlayerPrefs.GetFloat("Theme-R");
            float g = PlayerPrefs.GetFloat("Theme-G");
            float b = PlayerPrefs.GetFloat("Theme-B");
            float a = PlayerPrefs.GetFloat("Theme-A");

            Color color = new Color(r, g, b, a);
            themeController.currentColor = color;
        }
    }
}