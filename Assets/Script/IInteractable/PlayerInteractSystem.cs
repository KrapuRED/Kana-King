using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractSystem : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    private Vector2 lastDirection = Vector2.right;

    private RaycastHit2D hit;
    [SerializeField] private GameObject interactIcon;

    public void SetDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
            lastDirection = dir.normalized;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, lastDirection, interactDistance, interactLayer);

        if (hit.collider != null)
        {
            interactIcon.SetActive(true);
        }
        else
        {
            interactIcon.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}