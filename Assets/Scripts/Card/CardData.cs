using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost;
    public Vector2Int size = new Vector2Int(1, 1);
    public int attack;
    public int health;
    public Sprite artwork;

    [TextArea(3, 10)]
    public string description;

    public virtual void ApplyEffect(Card target) { }
}