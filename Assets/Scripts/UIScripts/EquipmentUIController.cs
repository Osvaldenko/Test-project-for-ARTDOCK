using UnityEngine;

public class EquipmentUIController : MonoBehaviour
{
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameController gameController;
    [Header("Equipment SlotsUI")]
    [SerializeField] private EquipmentSlotUI weaponSlot;
    [SerializeField] private EquipmentSlotUI headArmorSlot;
    [SerializeField] private EquipmentSlotUI bodyArmorSlot;
    [SerializeField] private EquipmentSlotUI bootsArmorSlot;

    public GameController GameController { get { return gameController; } }
    public GameObject EquipmentPanel { get { return equipmentPanel; } }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        weaponSlot.Setup(gameController.PlayerEquipment.GetEquipedItem(GearType.Weapon), this);
        headArmorSlot.Setup(gameController.PlayerEquipment.GetEquipedItem(GearType.HeadArmor), this);
        bodyArmorSlot.Setup(gameController.PlayerEquipment.GetEquipedItem(GearType.BodyArmor), this);
        bootsArmorSlot.Setup(gameController.PlayerEquipment.GetEquipedItem(GearType.Boots), this);
    }

    public void EquipItem(BaseItem item)
    {
        if (gameController.PlayerEquipment.GetEquipedItem((item as GearItem).GearType)) DropItem((item as GearItem).GearType);
        gameController.PlayerEquipment.EquipItem(item);
        UpdateUI();
    }
    public void DropItem(GearType itemToDrop)
    {
        gameController.CreateDropedItem(itemToDrop);
        UpdateUI();
    }
}