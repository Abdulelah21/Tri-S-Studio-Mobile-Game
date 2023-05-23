using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 300;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death, such as triggering a death animation or game over screen
        // ...
    }
}