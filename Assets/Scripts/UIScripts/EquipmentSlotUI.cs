using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rootRectTransform;
    [SerializeField] private Image icon;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform dragRectTransform;
    private EquipmentUIController equipmentUIController;
    private GearType thisSlotGearType;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        dragRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
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
        GameController gameController = equipmentUIController.GameController;
        InventoryUIController inventoryUIcontroller = gameController.InventoryUIController;

        if (inventoryUIcontroller != null && RectTransformUtility.RectangleContainsScreenPoint(inventoryUIcontroller.InventoryPanel.GetComponent<RectTransform>(), Input.mousePosition))
        {
            if (gameController.PlayerInventory.AddItem(gameController.PlayerEquipment.GetEquipedItem(thisSlotGearType), 1))
            {
                gameController.PlayerEquipment.UnEquipItem(thisSlotGearType);
                equipmentUIController.UpdateUI();
                inventoryUIcontroller.UpdateUI();
            }
        }
        else if(!RectTransformUtility.RectangleContainsScreenPoint(equipmentUIController.EquipmentPanel.GetComponent<RectTransform>(), Input.mousePosition))
        {
            equipmentUIController.DropItem(thisSlotGearType); // Выбрасываем предмет
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        dragRectTransform.anchoredPosition = rootRectTransform.anchoredPosition;
        equipmentUIController.UpdateUI();
    }
    public void Setup(GearItem equipment, EquipmentUIController _equipmentUIController)
    {
        equipmentUIController = _equipmentUIController;
        if (equipment != null)
        {
            icon.enabled = true;
            icon.sprite = equipment.ItemSprite;
            thisSlotGearType = equipment.GearType;
        }
        else
        {
            icon.enabled = false;
            icon.sprite = null;
        }
    }
}