using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory targetInventory;
    [SerializeField] private List<InventoryUISlot> _UISlots;
    [SerializeField] private RectTransform pointerRectTransform;
    private List<RectTransform> slotsRectTransform;
    private void OnEnable()
    {
        if (targetInventory == null)
        {
            return;
        }
        targetInventory.OnSlotInfoChanged += UpdateSlot;
        targetInventory.OnSlotSelected += PointerUpdate;
    }

    private void OnDisable()
    {
        if (targetInventory != null)
        {
            targetInventory.OnSlotInfoChanged -= UpdateSlot;
            targetInventory.OnSlotSelected -= PointerUpdate;
        }
    }
    private void Awake()
    {
        slotsRectTransform = new List<RectTransform>(_UISlots.Count);
        for (int i = 0; i < _UISlots.Count; i++)
        {
            slotsRectTransform.Add(_UISlots[i].GetComponent<RectTransform>());
        }
    }
    private void UpdateSlot(int slotIndex, InventorySlot slot)
    {
        if (slot.ItemsInSlotCount == 0)
            _UISlots[slotIndex].CountText.SetText("");
        else
            _UISlots[slotIndex].CountText.SetText(slot.ItemsInSlotCount.ToString());

        if (slot.ItemInSlot == null)
            _UISlots[slotIndex].ItemIcon.sprite = null;
        else
            _UISlots[slotIndex].ItemIcon.sprite = slot.ItemInSlot.Icon;
    }

    private void PointerUpdate(int slotIndex)
    {
        pointerRectTransform.position = slotsRectTransform[slotIndex].position;
    }
}
