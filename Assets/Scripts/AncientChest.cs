using UnityEngine;
using System.Collections;

public class AncientChest : MonoBehaviour, IInteractable
{
    public ResourceCounter resourceCounter;
    public int requiredSymbols = 5;

    public Transform lidPivot;
    public float openAngle = 100f;
    public float openSpeed = 90f;

    public AudioSource openSound;

    private bool isOpen;
    private bool isOpening;

    public string GetPromptText()
    {
        if(isOpen || isOpening)
        {
            return "";
        }

        int have = resourceCounter != null ? resourceCounter.SymbolCount : 0;
        return have + "/" + requiredSymbols + " Ancient Symbols";
    }

    public void Interact()
    {
        if(isOpen || isOpening)
        {
            return;
        }
        if(resourceCounter == null || resourceCounter.SymbolCount < requiredSymbols)
        {
            return; // not enough symbols yet - nothing happens
        }

        isOpening = true;
        if(openSound != null)
        {
            openSound.Play();
        }
        StartCoroutine(OpenChestRoutine());
    }

    private IEnumerator OpenChestRoutine()
    {
        float rotated = 0f;
        while(rotated < openAngle)
        {
            float step = Mathf.Min(openSpeed * Time.deltaTime, openAngle - rotated);
            if(lidPivot != null)
            {
                lidPivot.Rotate(Vector3.right, -step);
            }
            rotated += step;
            yield return null;
        }

        isOpen = true;
        isOpening = false;

        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnVaultOpened();
        }
    }
}
