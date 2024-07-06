using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardShop : MonoBehaviour
{
    public List<CardData> availableCards = new List<CardData>();
    public GameObject cardPrefab;
    public Transform shopContainer;
    public int cardsPerRefresh = 3;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log($"Available cards: {availableCards.Count}");
        if (availableCards.Count == 0)
        {
            Debug.LogWarning("No available cards in the shop. Please assign some in the Inspector.");
        }
        RefreshShop();
    }

    public void RefreshShop()
    {
        Debug.Log("Refreshing shop");
        foreach (Transform child in shopContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < cardsPerRefresh && availableCards.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            CardData cardData = availableCards[randomIndex];
            
            GameObject cardObject = Instantiate(cardPrefab, shopContainer);
            Card cardInstance = cardObject.GetComponent<Card>();
            
            if (cardInstance == null)
            {
                Debug.LogError("Card component not found on instantiated prefab");
                continue;
            }
            
            cardInstance.data = cardData;
            cardInstance.UpdateVisuals();

            Button buyButton = cardObject.GetComponent<Button>();
            if (buyButton != null)
            {
                buyButton.onClick.AddListener(() => BuyCard(cardInstance));
            }
            else
            {
                Debug.LogWarning("Buy button not found on card prefab");
            }

            Debug.Log($"Created card: {cardData.cardName}");
        }
    }

    public void BuyCard(Card card)
    {
        if (gameManager.BuyCard(card))
        {
            gameManager.AddCardToHand(card);
            RefreshShop();
        }
        else
        {
            Debug.Log("Not enough money to buy this card!");
        }
    }
}