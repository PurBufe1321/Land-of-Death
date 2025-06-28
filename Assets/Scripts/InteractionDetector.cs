using UnityEngine;
using UnityEngine.InputSystem;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using UnityEngine.Windows;

public class InteractionDetector : MonoBehaviour
{
    private Clickable interactableInRange = null;
    public GameObject interactionIconNormal;
    public GameObject interactionIconCard;

    private void Start()
    {
        interactionIconNormal.SetActive(false);
        interactionIconCard.SetActive(false);
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
            if (collision == GameObject.FindGameObjectWithTag("card"))
            {
                interactionIconCard.SetActive(true);
                interactableInRange = interactable;
            }
            else
            {
                interactionIconNormal.SetActive(true);
                interactableInRange = interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Clickable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIconNormal.SetActive(false);
            interactionIconCard.SetActive(false);
        }
    }
}
