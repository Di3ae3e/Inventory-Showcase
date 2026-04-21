using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotsCount = 1;
    private int selectedSlotIndex = 0;

    public event Action<int> OnSlotSelected;
    public event Action<int,InventorySlot> OnSlotInfoChanged;

    private List<InventorySlot> slots;

    //private void OnEnable()
    //{
    //    InGameItem.OnItemInteracted += AddItemToInventory;
    //}
    //private void OnDisable()
    //{
    //    InGameItem.OnItemInteracted -= AddItemToInventory;
    //}
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
    public bool IsItemAddedToInventory(Item itemToAdd, int amount)
    {
        int indexOfFirstEmptySlot = -1;
        bool hasEmptySlots = false;
        //foreach (InventorySlot slot in slots)
        for(int i = 0; i < slotsCount; i++)
        {
            if (slots[i].ItemInSlot == itemToAdd)
            {
                slots[i].ItemsInSlotCount += amount;
                OnSlotInfoChanged?.Invoke(i, slots[i]);
                return true;
            }
            else if (slots[i].ItemInSlot == null && indexOfFirstEmptySlot == -1)
            {
                indexOfFirstEmptySlot = i;
                hasEmptySlots = true;
            }
        }
        if (hasEmptySlots)
        {
            slots[indexOfFirstEmptySlot].ItemInSlot = itemToAdd;
            slots[indexOfFirstEmptySlot].ItemsInSlotCount = amount;
            OnSlotInfoChanged?.Invoke(indexOfFirstEmptySlot, slots[indexOfFirstEmptySlot]);

            return true;
        }
        else
        {
            Debug.LogWarning("<color=red>No Empty Slots!</color>");
            return false;
        }
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
    public bool IsManagedToPickUp()
    {
        bool isManagedToPickUp = false;
        return isManagedToPickUp;
    }
}
