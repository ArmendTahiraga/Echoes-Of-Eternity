using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour {
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Animator transitionAnimator;

    public void LoadLevel(string levelToLoad) {
        gameUI.SetActive(false);
        transitionAnimator.SetTrigger("FadeIn");
        StartCoroutine(StartLoading(levelToLoad));
    }

    private IEnumerator StartLoading(string levelToLoad) {
        yield return new WaitForSeconds(1f);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    private IEnumerator LoadLevelAsync(string levelToLoad) {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone) {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.99f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}