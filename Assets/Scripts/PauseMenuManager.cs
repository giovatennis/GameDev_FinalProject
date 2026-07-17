using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject startMenuPanel; // assign the SAME start menu panel used by StartMenuManager
    public ThirdPersonController thirdPersonController;
    public GameSaveManager saveManager;
    public StartMenuManager startMenuManager;
    public DayNightMusic dayNightMusic;

    private bool isPaused;

    // Update is called once per frame
    void Update()
    {
        // Never toggle pause while the start menu itself is showing
        if(startMenuPanel != null && startMenuPanel.activeSelf)
        {
            return;
        }

        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Hook this up to the pause panel's Resume button, if you add one
    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(thirdPersonController != null)
        {
            thirdPersonController.InputLocked = false;
            thirdPersonController.CursorControlLocked = false;
        }

        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    // Hook this up to the pause panel's Quit button
    public void OnQuitPressed()
    {
        // Save the exact current state before leaving, so Continue picks up right here
        if(saveManager != null)
        {
            saveManager.SaveGame();
        }

        isPaused = false;

        if(thirdPersonController != null)
        {
            thirdPersonController.InputLocked = false;
            thirdPersonController.CursorControlLocked = false;
        }

        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if(dayNightMusic != null)
        {
            dayNightMusic.StopMusic();
        }

        if(startMenuManager != null)
        {
            startMenuManager.OpenMenu();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(thirdPersonController != null)
        {
            thirdPersonController.InputLocked = true;
            thirdPersonController.CursorControlLocked = true;
        }

        if(pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }
}