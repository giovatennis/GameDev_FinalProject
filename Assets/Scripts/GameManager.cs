using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SaveSystem.DeleteSave();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SaveSystem.DeleteSave();
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