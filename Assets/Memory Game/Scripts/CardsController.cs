using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsController : MonoBehaviour {
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TextMeshProUGUI countText;
    private List<Sprite> spritePairs;
    private Card firstSelected;
    private Card secondSelected;
    private int matchCounts;

    void Start() {
        PrepareSprites();
        CreateCards();
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
        if (card.isSelected == false) {
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
    }

    private IEnumerator CheckMatching(Card firstSelectedCard, Card secondSelectedCard) {
        yield return new WaitForSeconds(0.2f);
        
        if (firstSelectedCard.iconSprite == secondSelectedCard.iconSprite) {
            matchCounts++;
            countText.text = countText.text.Split(":")[0] + ": " + matchCounts;
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
}