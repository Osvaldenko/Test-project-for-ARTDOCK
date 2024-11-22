using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacteristicTypes
{
    Strength,
    Armor,
    Agility
}
public enum GearType
{
    HeadArmor,
    BodyArmor,
    Boots,
    Weapon
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/GearItem")]
public class GearItem : BaseItem
{
    [SerializeField] private GearType gearType;
    [SerializeField] private List<characteristic> characteristicsList;

    public GearType GearType { get { return gearType; } }
    public List<characteristic> CharacteristicsList { get { return characteristicsList; } }

    [Serializable] public struct characteristic
    {
        public PlayerCharacteristicTypes characteristicType;
        public int value;
    }
}