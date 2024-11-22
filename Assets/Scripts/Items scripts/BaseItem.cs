using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected Sprite itemSprite;
    [SerializeField] protected int inventoryStackCount = 1;

    public string ItemName { get { return itemName; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public int InventoryStackCount { get { return inventoryStackCount; } }
}