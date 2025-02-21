using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsController : MonoBehaviour, MiniGame {
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject memoryGameCanvas;
    private List<Sprite> spritePairs;
    private Card firstSelected;
    private Card secondSelected;
    private int matchCounts;
    private float timeRemaining = 60f;
    private int totalPairs;
    public bool hasMiniGameStarted;

    public void StartGame() {
        hasMiniGameStarted = true;
        memoryGameCanvas.SetActive(true);
        PrepareSprites();
        CreateCards();
        totalPairs = sprites.Length;
        StartCoroutine(TimerCountdown());
        gameOverPanel.SetActive(false);
    }

    public bool GetHasMiniGameStarted() {
        return hasMiniGameStarted;
    }
    
    private void PrepareSprites() {
        spritePairs = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++) {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }

        ShuffleSprites(spritePairs);
    }

    void CreateCards() {
        for (int i = 0; i < spritePairs.Count; i++) {
            Card card = Instantiate(cardPrefab, gridContainer);
            card.SetIconSprite(spritePairs[i]);
            card.controller = this;
        }
    }

    public void SetSelected(Card card) {
        if (!hasMiniGameStarted || card.isSelected) return;

        card.Show();

        if (firstSelected == null) {
            firstSelected = card;
            return;
        }

        if (secondSelected == null) {
            secondSelected = card;
            StartCoroutine(CheckMatching(firstSelected, secondSelected));

            firstSelected = null;
            secondSelected = null;
        }
    }

    private IEnumerator CheckMatching(Card firstSelectedCard, Card secondSelectedCard) {
        yield return new WaitForSeconds(0.2f);

        if (firstSelectedCard.iconSprite == secondSelectedCard.iconSprite) {
            matchCounts++;
            countText.text = countText.text.Split(":")[0] + ": " + matchCounts;

            if (matchCounts == totalPairs) {
                EndGame();
            }
        } else {
            firstSelectedCard.Hide();
            secondSelectedCard.Hide();
        }
    }

    private void ShuffleSprites(List<Sprite> sprites) {
        for (int i = sprites.Count - 1; i >= 0; i--) {
            int randomIndex = Random.Range(0, i + 1);
            (sprites[i], sprites[randomIndex]) = (sprites[randomIndex], sprites[i]);
        }
    }

    private IEnumerator TimerCountdown() {
        while (timeRemaining > 0 && hasMiniGameStarted) {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            timerText.text = "Time: " + timeRemaining.ToString("0");
        }

        if (hasMiniGameStarted) {
            EndGame();
        }
    }

    private void EndGame() {
        hasMiniGameStarted = false;
        gameOverPanel.SetActive(true);
        memoryGameCanvas.SetActive(false);
        GameObject.Find("PlayerCam").GetComponent<PlayerCam>().enableCursor = false;

        if (matchCounts >= 6) {
            gameOverText.text = "You matched " + matchCounts + " pairs!\nYou get all the needed documents!";
        } else if (matchCounts >= 3) {
            gameOverText.text = "You matched " + matchCounts + " pairs!\nYou only get part of the documents.";
        } else {
            gameOverText.text = "You matched " + matchCounts + " pairs.\nYou don’t get the needed documents.";
        }
    }
}