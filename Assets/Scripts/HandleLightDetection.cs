using System;
using UnityEngine;

public class HandleLightDetection : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameOver();
        }
    }

    private void GameOver() {
        Debug.Log("Game Over");
    }
}