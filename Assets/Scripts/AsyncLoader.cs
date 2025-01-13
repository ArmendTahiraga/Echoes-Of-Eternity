using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour {
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Slider loadingSlider;

    public void LoadLevel(string levelToLoad) {
        gameUI.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad) {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone) {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}