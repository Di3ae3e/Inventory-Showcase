using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotsCount = 1;
    private int selectedSlotIndex = 0;

    public event Action<int> OnSlotSelected;
    public event Action<int, InventorySlot> OnSlotInfoChanged;

    private List<InventorySlot> slots;

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
        if (Input.GetKeyDown(KeyCode.Q))
            RemoveItem();
    }
    public int GetAddedCount(Item itemToAdd, int amount)
    {
        int addedItemsAmount = 0;
        for (int i = 0; i < slotsCount; i++)
        {
            if (slots[i].ItemInSlot == itemToAdd)
            {
                int added = GetAddedItemsInSlot(itemToAdd, amount, i);
                addedItemsAmount += added;
                amount -= added;
            }
        }
        if (amount != 0)
        {
            for (int i = 0; i < slotsCount; i++)
            {
                if (slots[i].ItemInSlot != null) continue;

                int added = GetAddedItemsInSlot(itemToAdd, amount, i);
                addedItemsAmount += added;
                amount -= added;
                if (amount == 0)
                    break;
            }
        }
        return addedItemsAmount;
    }
    private int GetAddedItemsInSlot(Item item, int amount, int slotIndex)
    {
        int addedAmount = 0;
        if (amount == 0) return 0;

        if (slots[slotIndex].ItemInSlot == null)
            slots[slotIndex].ItemInSlot = item;
        else if (slots[slotIndex].ItemInSlot != item)
        {
            return 0;
        }
        int remainingSpace = slots[slotIndex].ItemInSlot.MaxStackSize - slots[slotIndex].ItemsInSlotCount;
        if (amount < remainingSpace)
        {
            slots[slotIndex].ItemsInSlotCount += amount;
            addedAmount += amount;
        }
        else
        {
            slots[slotIndex].ItemsInSlotCount += remainingSpace;
            addedAmount += remainingSpace;
        }

        OnSlotInfoChanged?.Invoke(slotIndex, slots[slotIndex]);
        return addedAmount;
    }
    public void RemoveItem()
    {
        if (slots[selectedSlotIndex].ItemInSlot != null)
        {
            slots[selectedSlotIndex].ItemInSlot = null;
            slots[selectedSlotIndex].ItemsInSlotCount = 0;

            OnSlotInfoChanged?.Invoke(selectedSlotIndex, slots[selectedSlotIndex]);
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
        OnSlotSelected?.Invoke(selectedSlotIndex);
    }
}
