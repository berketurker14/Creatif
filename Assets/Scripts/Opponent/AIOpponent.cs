using UnityEngine;
using System.Collections.Generic;

public class AIOpponent : MonoBehaviour
{
    public Grid aiGrid;
    public int aiMoney = 100;
    public List<CardData> availableCards;
    public GameObject cardPrefab;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        aiGrid = GetComponent<Grid>();
        if (aiGrid == null)
        {
            aiGrid = gameObject.AddComponent<Grid>();
        }
    }

    public void TakeTurn()
    {
        // Simple AI: randomly place cards until out of money
        while (aiMoney > 0 && availableCards.Count > 0)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            CardData cardDataToPlace = availableCards[randomIndex];
            
            if (cardDataToPlace.cost <= aiMoney)
            {
                int x = Random.Range(0, aiGrid.width);
                int y = Random.Range(0, aiGrid.height);
                
                GameObject cardObject = Instantiate(cardPrefab, aiGrid.transform);
                Card cardToPlace = cardObject.GetComponent<Card>();
                cardToPlace.data = cardDataToPlace;
                cardToPlace.UpdateVisuals();
                
                if (aiGrid.PlaceCard(cardToPlace, x, y))
                {
                    aiMoney -= cardDataToPlace.cost;
                }
                else
                {
                    Destroy(cardObject);
                }
            }
            else
            {
                break; // Can't afford any more cards
            }
        }

        Debug.Log($"AI ended turn with {aiMoney} money left");
    }
}