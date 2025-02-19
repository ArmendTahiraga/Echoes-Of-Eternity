using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class CardsController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridContainer;
    [SerializeField] Sprite[] sprites;

    private List<Sprite> spritePairs;

    private Card firstSelected;
    private Card secondSelected;

    private int matchCounts;
    // Start is called before the first frame update
    void Start()
    {
        PrepareSprites();
        CreateCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        ShuffleSprites(spritePairs);
    }

    void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridContainer);
            card.SetIconSprite(spritePairs[i]);
            card.controller = this;
        }
    }

    public void SetSelected(Card card)
    {
        if (card.isSelected == false)
        {
            card.Show();
            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }

            if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatching(firstSelected, secondSelected));
                firstSelected = null;
                secondSelected = null;
            }
        }
    }

    IEnumerator CheckMatching(Card a, Card b)
    {
        yield return new WaitForSeconds(0.2f);
        if (a.iconSprite == b.iconSprite)
        {
            matchCounts++;
        }
        else
        {
            a.Hide();
            b.Hide();
        }
    }

    void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = sprites.Count - 1; i >= 0; i--)
        {
            int randomIndex = Random.Range(0, i+1);
            Sprite temp = sprites[i];
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = temp;
        }
    }
  
}
