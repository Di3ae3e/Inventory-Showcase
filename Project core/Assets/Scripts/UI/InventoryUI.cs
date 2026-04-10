using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventoryUISlot> _UISlots;
    [SerializeField] private RectTransform pointerRTransform;

    private void OnEnable()
    {
        Inventory.OnSlotInfoChanged += UpdateSlot;
        Inventory.OnActiveSlotChanged += PointerUpdate;
    }

    private void OnDisable()
    {
        Inventory.OnSlotInfoChanged -= UpdateSlot;
        Inventory.OnActiveSlotChanged -= PointerUpdate;
    }
    private void UpdateSlot(int slotIndex, InventorySlot slot)
    {
        _UISlots[slotIndex].CountText.text = slot.ItemsInSlotCount.ToString();
        if (slot.ItemInSlot == null)
            _UISlots[slotIndex].ItemIcon.sprite = null;
        else
            _UISlots[slotIndex].ItemIcon.sprite = slot.ItemInSlot.Icon;
    }

    private void PointerUpdate(int slotIndex)
    {
        pointerRTransform.position = _UISlots[slotIndex].GetComponent<RectTransform>().position;
    }
}
