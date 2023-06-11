using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }
    //Constructor
    public PlayerHealth(int health, int maxHealth)
    {
        currentHealth = health;
        this.maxHealth = maxHealth;
    }




    //Methods
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeHeal(int heal)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += heal;
        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        // Handle player death, such as triggering a death animation or game over screen
        // ...
    }
}