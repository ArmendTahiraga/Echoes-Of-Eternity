﻿using UnityEngine;

public class CheckVolume : MonoBehaviour {
    public void Start() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    public void UpdateVolume() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }
}