using UnityEngine;
using UnityEngine.InputSystem;

public class DebugDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(30);
            }
        }
    }
}