using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class Card : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite hiddenIconSprite;
    public Sprite iconSprite;
    public bool isSelected;
    public CardsController controller;
    float padding = 0.3f; 
    
    private void Start() {
        iconImage.sprite = hiddenIconSprite;
        iconImage.rectTransform.anchorMin = Vector2.zero;
        iconImage.rectTransform.anchorMax = Vector2.one;
        iconImage.rectTransform.offsetMin = new Vector2(padding, padding); 
        iconImage.rectTransform.offsetMax = new Vector2(-padding, -padding); 
    }

    public void OnCardClick() {
        controller.SetSelected(this);
    }

    public void SetIconSprite(Sprite sprite) {
        iconSprite = sprite;
    }

    public void Show() {
        Tween.Rotation(transform, new Vector3(0f, 180f, 0f), 0.2f);
        Tween.Delay(0.1f, () => iconImage.sprite = iconSprite);
        isSelected = true;
        


    }

    public void Hide() {
        Tween.Rotation(transform, new Vector3(0f, 0f, 0f), 0.5f);
        Tween.Delay(0.1f, () => {
                iconImage.sprite = hiddenIconSprite;
                isSelected = false;
            }
        );
    }
}