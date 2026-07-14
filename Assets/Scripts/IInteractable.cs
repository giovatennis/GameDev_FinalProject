public interface IInteractable
{
    // What the prompt UI should show. Return "" (empty string) to show no prompt.
    string GetPromptText();

    // Called when the player presses the interact button while this is the closest interactable.
    void Interact();
}
