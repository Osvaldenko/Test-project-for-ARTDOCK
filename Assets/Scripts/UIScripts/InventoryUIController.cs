using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject inventorySlotUIPrefab;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameController gameController;

    public GameObject InventoryPanel { get { return inventoryPanel; } }
    public GameController GameController { get { return gameController; } }

    private void OnEnable()
    {
        PickUpItemController.OnItemPickedUp += OnItemPickedUp;
    }
    private void OnDisable()
    {
        PickUpItemController.OnItemPickedUp -= OnItemPickedUp;
    }

    private void OnItemPickedUp()
    {
        UpdateUI();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < gameController.PlayerInventory.Slots.Count; i++)
        {
            var slot = gameController.PlayerInventory.Slots[i];
            GameObject slotObj = Instantiate(inventorySlotUIPrefab, inventoryGrid);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            slotUI.Setup(i, this, slot);
        }
    }
    public void SwapUIItems(int fromIndex, int toIndex)
    {
        gameController.PlayerInventory.MoveItemWithItem(fromIndex, toIndex);
        UpdateUI();
    }
    public void DropItem(int slotIndex)
    {
        gameController.CreateDropedItem(slotIndex);
        UpdateUI();
    }
    public void UseItem(int slotIndex)
    {
        gameController.UseItem(slotIndex);
        UpdateUI();
    }
}