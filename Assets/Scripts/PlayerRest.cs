using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;

public class PlayerRest : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public PlayerHealth playerHealth;
    public ThirdPersonController thirdPersonController;
    public Animator animator;


    public TextMeshProUGUI restPromptText;

    public int healPerTick = 5;
    public float tickInterval = 1f;

    private bool isResting;
    private float tickTimer;


    // Update is called once per frame
    void Update()
    {
     
        UpdatePrompt();

        if(isResting)
        {
            HandleResting();
        }
    }

    private void UpdatePrompt()
{
    if(restPromptText == null || dayNightCycle == null || playerHealth == null)
    {
        return;
    }

    if(isResting)
    {
        restPromptText.text = "Resting...";
        restPromptText.gameObject.SetActive(true);
        return;
    }

    bool canRest = !dayNightCycle.isNight && !playerHealth.IsAtFullHealth;

    if(canRest)
    {
        restPromptText.text = "Press F to rest";
        restPromptText.gameObject.SetActive(true);
    }
    else
    {
        restPromptText.gameObject.SetActive(false);
    }
}

    // Hook this up to a "Rest" Input Action bound to F in your Input Actions asset
    public void OnRest(InputValue value)
    {
        if(!value.isPressed)
        {
            return;
        }
        if(isResting)
        {
            return;
        }
        if(dayNightCycle == null || dayNightCycle.isNight)
        {
            return;
        }
        if(playerHealth == null || playerHealth.IsAtFullHealth)
        {
            return;
        }

        StartResting();
    }

    private void StartResting()
    {
        isResting = true;
        tickTimer = 0f;

        if(thirdPersonController != null)
        {
            thirdPersonController.InputLocked = true;
        }
        if(animator != null)
        {
            animator.SetBool("IsResting", true);
        }
        if(restPromptText != null)
        {
            restPromptText.gameObject.SetActive(false);
        }
    }

    private void HandleResting()
    {
        if(dayNightCycle != null && dayNightCycle.isNight)
        {
            StopResting();
            return;
        }

        if(playerHealth != null && playerHealth.IsAtFullHealth)
        {
            StopResting();
            return;
        }

        tickTimer += Time.deltaTime;
        if(tickTimer >= tickInterval)
        {
            tickTimer -= tickInterval;
            if(playerHealth != null)
            {
                playerHealth.Heal(healPerTick);
            }
        }
    }

    private void StopResting()
    {
        isResting = false;

        if(thirdPersonController != null)
        {
            thirdPersonController.InputLocked = false;
        }
        if(animator != null)
        {
            animator.SetBool("IsResting", false);
        }
    }
}
