using UnityEngine;
using System;
public class InGameItem : MonoBehaviour, IInteractable
{
    public Item Item;
    public int ItemsCount = 1;

    public event Action<InGameItem> OnItemInteracted;
    public void Interact()
    {
        Debug.Log("Interacted");
        OnItemInteracted?.Invoke(gameObject.GetComponent<InGameItem>());
    }
}
