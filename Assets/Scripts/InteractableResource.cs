using UnityEngine;

public class InteractableResource : MonoBehaviour, IInteractable
{
    public string resourceName = "Relic";
    public int amountPerCollect = 1;

    public int usesRemaining = 1;

    public string promptText = "Press E to gather";

    public bool destroyWhenEmpty = true;

    private ResourceCounter resourceCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceCounter = FindFirstObjectByType<ResourceCounter>();
    }

    public string GetPromptText()
    {
        return usesRemaining > 0 ? promptText : "";
    }

    public void Interact()
    {
        if(usesRemaining <= 0)
        {
            return;
        }
        if(resourceCounter != null)
        {
            resourceCounter.AddResource(resourceName, amountPerCollect);
        }

        usesRemaining--;

        if(usesRemaining <= 0 && destroyWhenEmpty == true)
        {
            gameObject.SetActive(false);
        }
    }
}
