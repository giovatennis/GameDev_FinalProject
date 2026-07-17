using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public Button continueButton;
    public GameSaveManager saveManager;
    public DayNightMusic dayNightMusic;
    public AudioSource menuMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenMenu();
    }

    // Shared entry point - called on first launch, and again if the player quits
    // back to the menu from the pause screen mid-game
    public void OpenMenu()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(startMenuPanel != null)
        {
            startMenuPanel.SetActive(true);
        }

        if(continueButton != null)
        {
            continueButton.interactable = SaveSystem.HasSave();
        }

        if(menuMusic != null)
        {
            menuMusic.loop = true;
            menuMusic.Play();
        }
    }

    // Hook this up to the Continue button's OnClick()
    public void OnContinuePressed()
    {
        if(saveManager != null)
        {
            saveManager.LoadGame();
        }
        CloseMenu();
    }

    // Hook this up to the New Game button's OnClick()
    public void OnNewGamePressed()
    {
        SaveSystem.DeleteSave();
        CloseMenu();
    }

    private void CloseMenu()
    {
        if(startMenuPanel != null)
        {
            startMenuPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(menuMusic != null)
        {
            menuMusic.Stop();
        }
        if(dayNightMusic != null)
        {
            dayNightMusic.StartMusic();
        }
    }
}
