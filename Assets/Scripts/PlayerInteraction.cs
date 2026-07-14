using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public TextMeshProUGUI promptText;

    private IInteractable currentInteractable;
    private Animator animator;

    private bool isInteracting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if(promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindNearbyInteractable();
    }

    private void FindNearbyInteractable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange);

        IInteractable closest = null;
        float closestDistance = Mathf.Infinity;

        foreach(Collider hit in hits)
        {
            IInteractable interactable = hit.GetComponentInParent<IInteractable>();
            if(interactable == null)
            {
                continue;
            }

            MonoBehaviour mb = interactable as MonoBehaviour;
            if(mb == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, mb.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }
        currentInteractable = closest;

        if(promptText == null)
        {
            return;
        }

        string prompt = (currentInteractable != null) ? currentInteractable.GetPromptText() : "";

        if(!string.IsNullOrEmpty(prompt) && !isInteracting)
        {
            promptText.text = prompt;
            promptText.gameObject.SetActive(true);
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }

    public void OnInteract(InputValue value)
    {
        if(!value.isPressed)
        {
            return;
        }
        if(currentInteractable == null || isInteracting)
        {
            return;
        }
        StartCoroutine(InteractRoutine());
    }

    private IEnumerator InteractRoutine()
    {
        isInteracting = true;

        if(promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
        if(animator != null)
        {
            animator.SetTrigger("Interact");
        }
        yield return new WaitForSeconds(0.8f);

        if(currentInteractable != null)
        {
            currentInteractable.Interact();
        }

        yield return new WaitForSeconds(0.3f);

        isInteracting = false;
    }
}
