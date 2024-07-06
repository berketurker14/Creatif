using UnityEngine;

public class DraggableCard : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Grid grid;
    private GameManager gameManager;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Vector3 mousePos = GetMouseWorldPosition();
        int gridX = Mathf.RoundToInt(mousePos.x);
        int gridY = Mathf.RoundToInt(mousePos.y);

        Card card = GetComponent<Card>();
        if (grid.PlaceCard(card, gridX, gridY))
        {
            gameManager.playerHand.Remove(card);
            gameManager.UpdateHandVisuals();
        }
        else
        {
            // Return to hand if placement is invalid
            gameManager.UpdateHandVisuals();
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}