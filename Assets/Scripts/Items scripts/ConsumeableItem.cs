using UnityEngine;

public enum PlayerStatsTypes
{
    HealthPoints,
    ManaPoints
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ConsumeableItem")]
public class ConsumeableItem : BaseItem
{
    [SerializeField] private PlayerStatsTypes playerStatToChange;
    [SerializeField] private int changeValue; //from minus to plus

    public PlayerStatsTypes PlayerStatToChange { get { return playerStatToChange; } }
    public int ChangeValue { get { return changeValue; } }

    public void Consume()
    {
        Debug.Log($"Используем {ItemName} + {changeValue} {playerStatToChange}!");
    }
}