using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask;

    private Inventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, interactableLayerMask))
            {
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.TryGetComponent(out InGameItem item))
                        {
                            //OnItemClicked?.Invoke(item);
                            playerInventory.AddItemToInventory(item);
                        }
                        //if (hit.collider.TryGetComponent(out IInteractable interactable))
                        //{
                        //    interactable.Interact();
                        //}
                    }
                }
            }
        }
    }
}
