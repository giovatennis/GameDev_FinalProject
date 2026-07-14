using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
{
    public TextMeshProUGUI symbolText;

    private int symbols;

    public int SymbolCount => symbols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        symbolText.text = "Ancient Symbols: " + symbols;
    }

    public void AddResource(string resourceName, int amount)
    {
        if(resourceName == "Symbol")
        {
            symbols += amount;
            UpdateUI();
        }
    }
}
