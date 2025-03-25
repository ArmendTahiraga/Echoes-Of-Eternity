using UnityEngine;

public class CheckVolume : MonoBehaviour {
    public void Start() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("GameVolume", 1);
    }

    public void UpdateVolume() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("GameVolume", 1);
    }
}