using System;

[Serializable]
public class InventorySlot
{
    private BaseItem item;
    private int quantity;

    public BaseItem Item { get { return item; } }
    public int Quantity { get { return quantity; } }

    public InventorySlot()
    {
        item = null;
        quantity = 0;
    }

    public void SetItem(BaseItem newItem)
    {
        item = newItem;
    }
    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
    public bool RemoveQuantity(int amount)
    {
        quantity -= amount;
        return quantity <= 0;
    }
    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
    public bool IsEmpty()
    {
        return item == null;
    }
}