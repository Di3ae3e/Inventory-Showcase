using UnityEngine;
using System;
public class InGameItem : MonoBehaviour, IInteractable
{
    public Item Item;
    public int ItemsCount = 1;

    public static event Action<InGameItem> OnItemInteracted;
    private void OnEnable()
    {
        if(ItemsCount > Item.StackSize)
            ItemsCount = Item.StackSize;
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out Inventory inventory))
        {
            if (inventory.IsItemAddedToInventory(Item, ItemsCount))
            {
                Destroy(gameObject);
            }
        }
    }
}
