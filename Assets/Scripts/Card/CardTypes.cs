using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Card", menuName = "Card Game/Attack Card")]
public class AttackCardData : CardData
{
    public override void ApplyEffect(Card target)
    {
        target.data.health -= this.attack;
    }
}

[CreateAssetMenu(fileName = "New Heal Card", menuName = "Card Game/Heal Card")]
public class HealCardData : CardData
{
    public int healAmount;

    public override void ApplyEffect(Card target)
    {
        target.data.health += healAmount;
    }
}

[CreateAssetMenu(fileName = "New Buff Card", menuName = "Card Game/Buff Card")]
public class BuffCardData : CardData
{
    public int attackBuff;

    public override void ApplyEffect(Card target)
    {
        target.data.attack += attackBuff;
    }
}