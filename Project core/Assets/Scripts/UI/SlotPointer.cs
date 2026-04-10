using UnityEngine;

public class SlotPointer : MonoBehaviour
{
    private void OnEnable()
    {
        UISlots.OnActiveSlotChanged += UpdatePointer;
    }
    private void UpdatePointer(RectTransform slotPosition)
    {
        transform.position = slotPosition.position;
    }
}
