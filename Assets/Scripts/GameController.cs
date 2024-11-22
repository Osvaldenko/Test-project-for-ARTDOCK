using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private PlayerController playerController;
    [Header("PlayerData")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private EquipmentController playerEquipment;
    [Header("UI")]
    [SerializeField] private InventoryUIController inventoryUIController;
    [SerializeField] private EquipmentUIController equipmentUIController;

    public InventoryUIController InventoryUIController { get { return inventoryUIController; } }
    public EquipmentUIController EquipmentUIController { get { return equipmentUIController; } }
    public Inventory PlayerInventory { get { return playerInventory; } }
    public EquipmentController PlayerEquipment { get { return playerEquipment; } }

    private void Awake()
    {
        playerInventory.CheckIfInventoryCreated();
    }
    public void CreateDropedItem(GearType itemToDrop)
    {
        GameObject droppedItem = Instantiate(droppedItemPrefab);
        droppedItem.GetComponent<PickUpItemController>().SetItem(playerEquipment.GetEquipedItem(itemToDrop), 1);
        droppedItem.transform.position = GetPlayerDropPosition();

        playerEquipment.UnEquipItem(itemToDrop);
    }
    public void CreateDropedItem(int slotIndex)
    {
        var slot = playerInventory.Slots[slotIndex];
        if (slot.IsEmpty()) return;

        GameObject droppedItem = Instantiate(droppedItemPrefab);
        droppedItem.GetComponent<PickUpItemController>().SetItem(slot.Item, slot.Quantity);
        droppedItem.transform.position = GetPlayerDropPosition();

        Debug.Log($"Выброшен {slot.Item.ItemName} на землю.");
        playerInventory.Slots[slotIndex].ClearSlot();
    }
    public void UseItem(int slotIndex)
    {
        var slot = playerInventory.Slots[slotIndex];
        if (slot.Item is ConsumeableItem consumableItem)
        {
            consumableItem.Consume();
            playerInventory.RemoveItem(slot.Item, 1);
        }
    }
    private Vector3 GetPlayerDropPosition()
    {
        Transform playerTransform = playerController.transform;
        return playerTransform.position + playerTransform.forward * 2;
    }
}