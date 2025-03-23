using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private GameObject grave;
    private Renderer renderer;
    private Color originalColor;

    private void Start() {
        renderer = grave.GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    private void StartFadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine() {
        float alpha = 1f;
    
        while (alpha > 0f) {
            alpha -= Time.deltaTime / fadeDuration;
            renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Max(alpha, 0f));
            yield return null;
        }

        grave.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartFadeOut();
        }
    }
}