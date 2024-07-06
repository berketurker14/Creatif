using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    private Card[,] grid;
    
    void Start()
    {
        grid = new Card[width, height];
        CreateGridVisuals();
    }
    
    public bool PlaceCard(Card card, int x, int y)
    {
        if (IsValidPlacement(card, x, y))
        {
            for (int i = 0; i < card.data.size.x; i++)
            {
                for (int j = 0; j < card.data.size.y; j++)
                {
                    grid[x + i, y + j] = card;
                }
            }
            card.transform.position = new Vector3(x, y, 0);
            return true;
        }
        return false;
    }
    
    private bool IsValidPlacement(Card card, int x, int y)
    {
        if (x < 0 || y < 0 || x + card.data.size.x > width || y + card.data.size.y > height)
        {
            return false;
        }
        
        for (int i = 0; i < card.data.size.x; i++)
        {
            for (int j = 0; j < card.data.size.y; j++)
            {
                if (grid[x + i, y + j] != null)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    public void ResolveCombat()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Card card = grid[x, y];
                if (card != null)
                {
                    ApplyEffectToAdjacent(card, x, y);
                    
                    Card target = FindTarget(x, y);
                    if (target != null)
                    {
                        card.ApplyEffect(target);
                        if (target.data.health <= 0)
                        {
                            RemoveCard(target);
                        }
                    }
                }
            }
        }
    }

    private void ApplyEffectToAdjacent(Card card, int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int nx = x + dx, ny = y + dy;
                if (nx >= 0 && nx < width && ny >= 0 && ny < height && grid[nx, ny] != null)
                {
                    card.ApplyEffect(grid[nx, ny]);
                    if (grid[nx, ny].data.health <= 0)
                    {
                        RemoveCard(grid[nx, ny]);
                    }
                }
            }
        }
    }

    private Card FindTarget(int x, int y)
    {
        if (y < height - 1)
        {
            return grid[x, y + 1];
        }
        return null;
    }

    public void RemoveCard(Card card)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == card)
                {
                    grid[x, y] = null;
                }
            }
        }
        Destroy(card.gameObject);
    }

    private void CreateGridVisuals()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                cell.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0.1f);
                cell.transform.parent = this.transform;
            }
        }
    }
}