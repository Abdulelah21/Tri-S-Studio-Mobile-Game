using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    private PlayerHealth playerHealth; // Reference to the player health in the PlayerHealth script

    public Image healthBarImage; // Reference to the health bar inner image
    float healthPercentage;

    [SerializeField]
    private Gradient ColorGradient;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        healthPercentage = (float)playerHealth.Health / playerHealth.MaxHealth;
        healthBarImage.color = ColorGradient.Evaluate(1 - healthBarImage.fillAmount);
        healthBarImage.fillAmount = healthPercentage;
    }
}
