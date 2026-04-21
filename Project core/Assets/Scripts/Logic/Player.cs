using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayerMask;
    private string interactedObject;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, interactableLayerMask))
            {
                if (hit.collider == null) return;

                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactedObject = hit.transform.name;
                    interactable.Interact(gameObject);
                }
            }
        }
    }
}
