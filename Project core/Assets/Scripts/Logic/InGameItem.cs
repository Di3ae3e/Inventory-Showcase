using UnityEngine;
using System;
public class InGameItem : MonoBehaviour, IInteractable
{
    public Item Item;
    public int ItemsCount = 1;

    public static event Action<InGameItem> OnItemInteracted;
    private void OnEnable()
    {
        if (ItemsCount > Item.MaxStackSize)
            ItemsCount = Item.MaxStackSize;
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out Inventory inventory))
        {
            int addedItemsCount = inventory.GetAddedCount(Item, ItemsCount);

            ItemsCount -= addedItemsCount;
            if (ItemsCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
