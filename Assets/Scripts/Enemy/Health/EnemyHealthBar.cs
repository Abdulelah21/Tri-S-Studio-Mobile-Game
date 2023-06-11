using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    private EnemyHealth enemyHealth; // Reference to the enemy's EnemyHealth script

    public Image healthBarImage; // Reference to the health bar inner image
    float healthPercentage;

    [SerializeField]
    private Gradient ColorGradient;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    // Update is called once per frame
    void Update()
    {
         healthPercentage = (float)enemyHealth.CurrentHealth / enemyHealth.MaxHealth;
        healthBarImage.color = ColorGradient.Evaluate(1 - healthBarImage.fillAmount);
        healthBarImage.fillAmount = healthPercentage;
    }
}
