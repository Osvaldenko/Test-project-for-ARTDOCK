using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptableObjects/PlayerInventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private int maxSlots = 4;

    private List<InventorySlot> slots = new List<InventorySlot>();

    public List<InventorySlot> Slots { get { return slots; } }

    public void CheckIfInventoryCreated()
    {
        //Debug.Log(slots.Count);
        if (slots.Count == 0)
        {
            for (int i = 0; i < maxSlots; i++)
            {
                slots.Add(new InventorySlot());
            }
        }
    }
    public bool AddItem(BaseItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (slot.Item == item)
            {
                if (slot.Quantity < slot.Item.InventoryStackCount)
                {
                    slot.AddQuantity(quantity);
                    return true;
                }
            }
        }
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                slot.AddQuantity(quantity);
                return true;
            }
        }
        Debug.Log("Инвентарь заполнен!");
        return false;
    }
    public bool RemoveItem(BaseItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (slot.Item == item)
            {
                if (slot.RemoveQuantity(quantity))
                {
                    slot.ClearSlot();
                }
                return true;
            }
        }

        Debug.Log("Предмет не найден в инвентаре!");
        return false;
    }
    public void MoveItemWithItem(int fromIndex, int toIndex)
    {
        if (fromIndex >= 0 && fromIndex < slots.Count && toIndex >= 0 && toIndex < slots.Count)
        {
            var temporarySlot = slots[toIndex];
            slots[toIndex] = slots[fromIndex];
            slots[fromIndex] = temporarySlot;
        }
    }
}