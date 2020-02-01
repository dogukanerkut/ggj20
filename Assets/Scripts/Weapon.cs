using UnityEngine;

public class Weapon : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

}
