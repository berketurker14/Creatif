using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData data;
    public TextMeshProUGUI cardText;
    public Image artworkImage;

    public int level = 1;

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (cardText != null && data != null)
        {
            cardText.text = $"{data.cardName}\nLv: {level}\nCost: {data.cost}\nATK: {data.attack}\nHP: {data.health}";
        }
        else
        {
            Debug.LogWarning("Card Text component or CardData is missing on " + gameObject.name);
        }

        if (artworkImage != null && data.artwork != null)
        {
            artworkImage.sprite = data.artwork;
        }
    }

    public void LevelUp()
    {
        level++;
        UpdateVisuals();
    }

    public void ApplyEffect(Card target)
    {
        data.ApplyEffect(target);
    }
}