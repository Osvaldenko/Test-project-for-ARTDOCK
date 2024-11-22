using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private int slotIndex;
    private InventoryUIController inventoryUIController;

    private RectTransform dragRectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        dragRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Setup(int index, InventoryUIController uiController, InventorySlot slot)
    {
        slotIndex = index;
        inventoryUIController = uiController;
        if (slot.Item != null)
        {
            icon.sprite = slot.Item.ItemSprite;
            icon.enabled = true;
            quantityText.text = slot.Quantity.ToString();
        }
        else
        {
            icon.enabled = false;
            quantityText.text = "";
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Если расходник, то используем
            inventoryUIController.UseItem(slotIndex);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        EquipmentUIController equipmentUI = inventoryUIController.GameController.EquipmentUIController;
        BaseItem currentItem = inventoryUIController.GameController.PlayerInventory.Slots[slotIndex].Item;

        if (equipmentUI != null && RectTransformUtility.RectangleContainsScreenPoint(equipmentUI.EquipmentPanel.GetComponent<RectTransform>(), Input.mousePosition))
        {
            if (currentItem is GearItem)
            {
                equipmentUI.EquipItem(currentItem);
                inventoryUIController.GameController.PlayerInventory.RemoveItem(currentItem, 1);
                inventoryUIController.UpdateUI();
            }
            else
            {
                ResetSlotUI();
            }
        }
        else if(!RectTransformUtility.RectangleContainsScreenPoint(inventoryUIController.InventoryPanel.GetComponent<RectTransform>(), Input.mousePosition))
        {
            inventoryUIController.DropItem(slotIndex); // Выбрасываем предмет
        }
        else
        {
            ResetSlotUI();
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot.slotIndex != slotIndex)
        {
            inventoryUIController.SwapUIItems(draggedSlot.slotIndex, slotIndex);
        }
    }

    private void ResetSlotUI() // возврат UI элемента обратно, где он был
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        inventoryUIController.UpdateUI();
    }
}