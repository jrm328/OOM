using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private int numberOfRays = 9;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable currentInteractable;

    void Update()
    {
        Cast360Raycast();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void Cast360Raycast()
    {
        float angleStep = 360f / numberOfRays;
        bool foundInteractable = false;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = AngleToVector2(angle);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, interactionRange, interactableLayer);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (interactable != currentInteractable)
                    {
                        if (currentInteractable != null)
                        {
                            currentInteractable.HideVisualCue();
                        }

                        currentInteractable = interactable;
                        currentInteractable.ShowVisualCue();
                    }
                    foundInteractable = true;
                    Debug.DrawLine(transform.position, hit.point, Color.green, 0.5f);
                    break;
                }
            }
        }

        if (!foundInteractable && currentInteractable != null)
        {
            currentInteractable.HideVisualCue();
            currentInteractable = null;
        }
    }

    private Vector2 AngleToVector2(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}

