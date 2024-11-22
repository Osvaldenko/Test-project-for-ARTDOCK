using System;
using UnityEngine;

public class PickUpItemController : MonoBehaviour
{
    public static event Action<bool> OnPickUpButton;
    public static event Action OnInventoryFull;
    public static event Action OnItemPickedUp;

    [SerializeField] private BaseItem item;
    [SerializeField] private int addQuantity = 1;
    [SerializeField] private Inventory playerInventory;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            ShowPickupUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ShowPickupUI(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        if (playerInventory != null && playerInventory.AddItem(item, addQuantity))
        {
            OnItemPickedUp?.Invoke();
            ShowPickupUI(false);
            Destroy(gameObject);
        }
        else
        {
            OnInventoryFull?.Invoke();
        }
    }
    private void ShowPickupUI(bool show)
    {
        OnPickUpButton?.Invoke(show);
    }

    public void SetItem(BaseItem newItem, int newQuantity)
    {
        item = newItem;
        addQuantity = newQuantity;
    }
}