using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObjects/PlayerEquipment")]
public class EquipmentController : ScriptableObject
{
    private GearItem equippedWeapon;
    private GearItem equippedHead;
    private GearItem equippedArmor;
    private GearItem equippedBoots;

    public GearItem GetEquipedItem(GearType gearType)
    {
        switch (gearType)
        {
            case GearType.Weapon:
                return equippedWeapon;
            case GearType.HeadArmor:
                return equippedHead;
            case GearType.BodyArmor:
                return equippedArmor;
            case GearType.Boots:
                return equippedBoots;
        }
        Debug.Log($"Ёкипировки типа {gearType} нет");
        return null;
    }
    public void UnEquipItem(GearType item)
    {
        switch(item)
        {
            case GearType.Weapon:
                equippedWeapon = null;
                break;
            case GearType.HeadArmor:
                equippedHead = null;
                break;
            case GearType.BodyArmor:
                equippedArmor = null;
                break;
            case GearType.Boots:
                equippedBoots = null;
                break;
        }
    }
    public void EquipItem(BaseItem item)
    {
        if (item is GearItem equipment)
        {
            switch (equipment.GearType)
            {
                case GearType.Weapon:
                        equippedWeapon = equipment;
                    break;
                case GearType.HeadArmor:
                        equippedHead = equipment;
                    break;
                case GearType.BodyArmor:
                        equippedArmor = equipment;
                    break;
                case GearType.Boots:
                        equippedBoots = equipment;
                    break;
            }
        }
        else
        {
            Debug.Log("Ётот предмет нельз€ экипировать.");
        }
    }
}