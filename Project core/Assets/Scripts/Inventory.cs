using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotsCount = 1;
    private List<InventorySlot> slots;

    public static event Action<Item> OnItemAdded;

    private int selectedSlotIndex = 0;

    private void Awake()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < slotsCount; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
    }
    private void Start()
    {
        Debug.Log(slots.Capacity);
    }
    private void OnEnable()
    {
        //InGameItem.OnItemInteracted += AddItemToInventory;
    }
    private void Update()
    {
        SlotSelect();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int slotsOccupied = 0;
            foreach (InventorySlot slot in slots)
            {
                if (slot.ItemInSlot != null)
                {
                    Debug.Log($"Slot {slots.IndexOf(slot)} contatins {slot.ItemInSlot.ItemName} in quantity {slot.ItemsInSlotCount}");
                    slotsOccupied++;
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
    }
}
