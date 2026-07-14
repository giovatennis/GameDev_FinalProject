using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI objectiveText;

    public ResourceCounter resourceCounter;
    public int symbolsNeeded = 5;

    private bool gameOver;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if(losePanel != null)
        {
            losePanel.SetActive(false);
        }
        UpdateObjectiveText();
    }

    // Update is called once per frame
   void Update()
{
    UpdateObjectiveText();

    if(gameOver && Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
    {
        RestartGame();
    }
}

    private void UpdateObjectiveText()
    {
        if(objectiveText == null || resourceCounter == null || gameOver)
        {
            return;
        }

        int have = resourceCounter.SymbolCount;
        objectiveText.text = "Ancient Symbols: " + have + " / " + symbolsNeeded + "\nBring them all to the chest.";
    }

    public void OnPlayerDeath()
    {
        if(gameOver)
        {
            return;
        }
        gameOver = true;
        Time.timeScale = 0f;
        if(losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    public void OnVaultOpened()
    {
        if(gameOver)
        {
            return;
        }
        gameOver = true;
        Time.timeScale = 0f;
        if(winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
