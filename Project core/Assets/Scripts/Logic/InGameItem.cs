using UnityEngine;
using System;
public class InGameItem : MonoBehaviour, IInteractable
{
    public Item Item;
    public int ItemsCount = 1;

    public static event Action<InGameItem> OnItemInteracted;
    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out Inventory inventory))
        {
            inventory.AddItemToInventory(gameObject.GetComponent<InGameItem>());
        }
    }
}
