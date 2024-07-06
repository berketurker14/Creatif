using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Grid playerGrid;
    public int playerMoney = 100;
    public int turnNumber = 0;
    public List<Card> playerHand = new List<Card>();
    public Transform handContainer;
    public AIOpponent aiOpponent;
    public GameObject cardPrefab;

    void Start()
    {
        playerGrid = GetComponent<Grid>();
        if (playerGrid == null)
        {
            playerGrid = gameObject.AddComponent<Grid>();
        }
    }

    public void EndTurn()
    {
        turnNumber++;
        playerMoney += 10; // Basic income per turn
        // Calculate interest
        int interest = Mathf.FloorToInt(playerMoney * 0.1f);
        playerMoney += interest;

        // Resolve player's combat
        playerGrid.ResolveCombat();

        // AI turn
        aiOpponent.TakeTurn();
        aiOpponent.aiGrid.ResolveCombat();

        Debug.Log($"Turn {turnNumber} ended. Player money: {playerMoney}");
    }

    public bool BuyCard(Card card)
    {
        if (playerMoney >= card.data.cost)
        {
            playerMoney -= card.data.cost;
            return true;
        }
        return false;
    }

    public void AddCardToHand(Card card)
    {
        GameObject newCardObject = Instantiate(cardPrefab, handContainer);
        Card newCard = newCardObject.GetComponent<Card>();
        newCard.data = card.data;
        newCard.UpdateVisuals();
        playerHand.Add(newCard);
        UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand[i].transform.localPosition = new Vector3(i * 1.2f, 0, 0);
        }
    }
}