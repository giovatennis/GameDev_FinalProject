using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    private int currentHealth;
    private float invulnerableTimer = 0f;
    private float invulnerableDuration = 0.5f;

    public bool IsDead { get; private set; }
    public bool IsAtFullHealth => currentHealth >= maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerableTimer > 0f)
        {
            invulnerableTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        if(IsDead || invulnerableTimer > 0f)
        {
            return;
        }

        currentHealth -= amount;
        invulnerableTimer = invulnerableDuration;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            IsDead = true;
            if(GameManager.Instance != null)
            {
                GameManager.Instance.OnPlayerDeath();
            }
        }
        UpdateUI();
    }

    public void Heal(int amount)
    {
        if(IsDead)
        {
            return;
        }
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
        if(healthText != null)
        {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth;
        }
    }
}
