using UnityEngine;
using System.Collections;
using TMPro;

public class GameSaveManager : MonoBehaviour
{
    public ResourceCounter resourceCounter;
    public PlayerHealth playerHealth;
    public DayNightCycle dayNightCycle;

    public float autoSaveInterval = 10f;

    public TextMeshProUGUI savingIndicatorText;
    public float savingIndicatorDuration = 1.2f;

    private float autoSaveTimer;
    private Coroutine savingIndicatorRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(savingIndicatorText != null)
        {
            savingIndicatorText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        autoSaveTimer += Time.deltaTime;
        if(autoSaveTimer >= autoSaveInterval)
        {
            autoSaveTimer = 0f;
            SaveGame();
        }
    }

    public void SaveGame()
    {
        if(resourceCounter == null || playerHealth == null || dayNightCycle == null)
        {
            return;
        }

        SaveData data = new SaveData
        {
            symbolCount = resourceCounter.SymbolCount,
            playerHealth = playerHealth.CurrentHealth,
            isNight = dayNightCycle.isNight
        };

        SaveSystem.Save(data);
        ShowSavingIndicator();
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        if(data == null)
        {
            return;
        }

        if(resourceCounter != null)
        {
            resourceCounter.SetSymbolCount(data.symbolCount);
        }
        if(playerHealth != null)
        {
            playerHealth.SetHealth(data.playerHealth);
        }
        if(dayNightCycle != null)
        {
            dayNightCycle.SetPhase(data.isNight);
        }
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSave();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    private void ShowSavingIndicator()
    {
        if(savingIndicatorText == null)
        {
            return;
        }

        if(savingIndicatorRoutine != null)
        {
            StopCoroutine(savingIndicatorRoutine);
        }
        savingIndicatorRoutine = StartCoroutine(SavingIndicatorRoutine());
    }

    private IEnumerator SavingIndicatorRoutine()
    {
        savingIndicatorText.text = "Saving...";
        savingIndicatorText.gameObject.SetActive(true);

        // WaitForSecondsRealtime so this still works even if called while Time.timeScale is 0
        // (e.g. saving right as the player quits to the menu)
        yield return new WaitForSecondsRealtime(savingIndicatorDuration);

        savingIndicatorText.gameObject.SetActive(false);
    }
}
