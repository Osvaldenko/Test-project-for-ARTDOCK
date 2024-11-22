using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pickUpButton;
    [SerializeField] private GameObject inventoryFullMessage;

    private Coroutine messageCoroutine;

    private void OnEnable()
    {
        PickUpItemController.OnPickUpButton += OnPickUpButton;
        PickUpItemController.OnInventoryFull += OnInventoryFull;
    }
    private void OnDisable()
    {
        PickUpItemController.OnPickUpButton -= OnPickUpButton;
        PickUpItemController.OnInventoryFull -= OnInventoryFull;
    }

    private void OnInventoryFull()
    {
        if (messageCoroutine == null) messageCoroutine = StartCoroutine(InventoryFullMessageCoroutine());
    }
    private void OnPickUpButton(bool isButtonActive)
    {
        pickUpButton.SetActive(isButtonActive);
    }

    private IEnumerator InventoryFullMessageCoroutine()
    {
        inventoryFullMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        inventoryFullMessage.SetActive(false);
        messageCoroutine = null;
    }
}