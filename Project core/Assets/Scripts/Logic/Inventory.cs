using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotsCount = 1;
    private int selectedSlotIndex = 0;

    public static event Action<RectTransform> OnActiveSlotChanged;

    private List<InventorySlot> slots;
    [SerializeField] private List<InventoryUISlot> uiSlot;
    private void Awake()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < slotsCount; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            SlotSelect();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.ItemInSlot != null)
                {
                    Debug.Log($"Slot {slots.IndexOf(slot)} contatins {slot.ItemInSlot.ItemName} in quantity {slot.ItemsInSlotCount}");
                }
                else
                {
                    Debug.LogWarning($"<color=yellow> Slot {slots.IndexOf(slot)} is empty!");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
            RemoveItem();
    }
    public void AddItemToInventory(InGameItem itemToAdd)
    {
        int indexOfFirstEmptySlot = -1;
        bool hasEmptySlots = false;
        foreach (InventorySlot slot in slots)
        {
            if (slot.ItemInSlot == itemToAdd.Item)
            {
                slot.ItemsInSlotCount += itemToAdd.ItemsCount;
                uiSlot[slots.IndexOf(slot)].ItemIcon.sprite = itemToAdd.Item.Icon;
                Destroy(itemToAdd.gameObject);
                return;
            }
            else if (slot.ItemInSlot == null && indexOfFirstEmptySlot == -1)
            {
                indexOfFirstEmptySlot = slots.IndexOf(slot);
                hasEmptySlots = true;
            }
        }
        if (hasEmptySlots)
        {
            slots[indexOfFirstEmptySlot].ItemInSlot = itemToAdd.Item;
            slots[indexOfFirstEmptySlot].ItemsInSlotCount = itemToAdd.ItemsCount;
            uiSlot[indexOfFirstEmptySlot].ItemIcon.sprite = itemToAdd.Item.Icon;
            Destroy(itemToAdd.gameObject);
        }
        else
        {
            Debug.LogWarning("<color=red>No Empty Slots!</color>");
        }
    }
    public void RemoveItem()
    {
        if (slots[selectedSlotIndex].ItemInSlot != null)
        {
            Debug.Log($"{slots[selectedSlotIndex].ItemInSlot.name} has been dropped, Slot {selectedSlotIndex} is empty");
            slots[selectedSlotIndex].ItemInSlot = null;
            slots[selectedSlotIndex].ItemsInSlotCount = 0;
            uiSlot[selectedSlotIndex].ItemIcon.sprite = null;
        }
        else
        {
            Debug.LogWarning($"Slot {selectedSlotIndex} is empty");
        }
    }

    private void SlotSelect()
    {
        selectedSlotIndex += (int)Input.mouseScrollDelta.y;
        if (selectedSlotIndex > slots.Count - 1)
        {
            selectedSlotIndex = 0;
        }
        if (selectedSlotIndex < 0)
        {
            selectedSlotIndex = slots.Count - 1;
        }
        OnActiveSlotChanged?.Invoke(uiSlot[selectedSlotIndex].GetComponent<RectTransform>());
    }
}
