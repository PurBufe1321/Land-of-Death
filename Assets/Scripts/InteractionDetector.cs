using UnityEngine;
using UnityEngine.InputSystem;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using UnityEngine.Windows;

public class InteractionDetector : MonoBehaviour
{
    private Clickable interactableInRange = null;
    public GameObject interactionIcon;

    private void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Clickable interactable) && interactable.CanClick())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Clickable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
