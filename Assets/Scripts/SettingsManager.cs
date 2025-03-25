using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    [SerializeField] private List<AudioSource> gameVolumeAudioSources;
    [SerializeField] private List<AudioSource> soundEffectsVolumeAudioSources;
    [SerializeField] private PlayerCam playerCam;

    private void Awake() {
        LoadSettings();
    }

    private void LoadSettings() {
        float gameVolumeSlider = PlayerPrefs.GetFloat("GameVolume", 1);
        float soundEffectsVolumeSlider = PlayerPrefs.GetFloat("SoundEffectsVolume", 1);
        float sensitivitySlider = PlayerPrefs.GetFloat("CameraSensitivity", 0.5f);
        
        foreach (AudioSource audioSource in gameVolumeAudioSources) {
            if (audioSource != null) {
                audioSource.volume *= gameVolumeSlider;
            }
        }

        foreach (AudioSource audioSource in soundEffectsVolumeAudioSources) {
            if (audioSource != null) {
                Debug.Log(soundEffectsVolumeSlider);
                Debug.Log(audioSource.volume);
                audioSource.volume *= soundEffectsVolumeSlider;
            }
        }

        if (playerCam != null) {
            playerCam.SetSensitivity(playerCam.GetSensitivityX() * (sensitivitySlider * 2), playerCam.GetSensitivityY() * (sensitivitySlider * 2));
        }
    }
}