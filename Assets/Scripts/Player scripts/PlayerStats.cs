using UnityEngine;

// я планировал менять статы при экипировке предметов или использовании расходников, но не успел доделать
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int healthPoints = 100;
    [SerializeField] private int manaPoints = 50;
    [SerializeField] private int strength = 1;
    [SerializeField] private int armor = 1;
    [SerializeField] private int agility = 1;

    public void ChangeHealth(int value)
    {
        healthPoints += value;
        if (healthPoints < 0) healthPoints = 0;
    }
    public void ChangeMana(int value)
    {
        manaPoints += value;
        if (manaPoints < 0) manaPoints = 0;
    }
    public void ChangeStrength(int value)
    {
        strength += value;
    }
    public void ChangeArmor(int value)
    {
        armor += value;
    }
    public void ChangeAgility(int value)
    {
        agility += value;
    }
}