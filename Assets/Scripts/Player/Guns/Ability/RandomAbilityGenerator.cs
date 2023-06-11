using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class RandomAbilityGenerator : MonoBehaviour
{
    public Ability[] abilities;
    public GameObject[] abilityPrefabs;
    public Transform abilityParent;
    [SerializeField]
    private PlayerGunSelector GunSelector;
    public AbilityUI interActionUI;

    private List<Ability> usedAbilities = new List<Ability>();

    private void Start()
    {
        interActionUI = FindObjectOfType<AbilityUI>();
        if (abilities == null || abilities.Length == 0)
        {
            Debug.LogError("Abilities array is null or empty.");
            return;
        }

        if (abilityPrefabs == null || abilityPrefabs.Length == 0)
        {
            Debug.LogError("Ability prefabs array is null or empty.");
            return;
        }

        // Generate a random ability for each ability prefab
        foreach (GameObject abilityPrefab in abilityPrefabs)
        {
            Ability ability = GetRandomAbility();
            usedAbilities.Add(ability);

            // Create the UIElements for the ability
            GameObject abilityObject = Instantiate(abilityPrefab, abilityParent);

            Text nameLabel = abilityObject.transform.Find("NameLabel").GetComponent<Text>();
            Text descriptionLabel = abilityObject.transform.Find("DescriptionLabel").GetComponent<Text>();
            Image image = abilityObject.transform.Find("Image").GetComponent<Image>();
            Button button = abilityObject.transform.Find("Button").GetComponent<Button>();

            nameLabel.text = ability.abilityName;
            descriptionLabel.text = ability.description;
            image.sprite = ability.image;

            // Add an onClick listener to the button to send the appropriate variable
            button.onClick.AddListener(delegate { 
                SendVariable(ability.abilityName);
                interActionUI.buttonIsClicked = true;
            });

        }
    }

    private Ability GetRandomAbility()
    {
        Ability ability = null;
        int maxAttempts = 10;

        // Try to get a random ability that hasn't been used yet
        for (int i = 0; i < maxAttempts; i++)
        {
            ability = abilities[Random.Range(0, abilities.Length)];

            if (!usedAbilities.Contains(ability))
            {
                break;
            }
            else
            {
                ability = null;
            }
        }

        // If all abilities have been used, reset the usedAbilities list
        if (ability == null)
        {
            usedAbilities.Clear();
            ability = abilities[Random.Range(0, abilities.Length)];
        }

        return ability;
    }

    private void SendVariable(string abilityName)
    {
        // Send the appropriate variable based on the ability name
        switch (abilityName)
        {
            case "Attack Damage":
                // Get the damage configuration object attached to the player gun
                DamageConfigurationScriptableObject damageConfig = FindObjectOfType<GunScriptableObject>().DamageConfig;


                // Add 5% to the current damage value
                float currentValue = damageConfig.DamageValue;
                float newValue = currentValue * 1.05f;

                // Set the new value of the damage configuration
                damageConfig.DamageValue = newValue;

                Debug.Log("Player new damage = " + newValue);

                break;
            case "Attack Speed":
                // Send attack speed variable to another script
                ShootConfiguraionScriptableObject shootConfig = FindObjectOfType<GunScriptableObject>().ShootConfig;

                // Add 5% to the current attack speed value
                float currentAttackSpeed = shootConfig.FireRate;
                float newAttackSpeed = currentAttackSpeed * 0.95f;

                // Set the new value of the damage configuration
                shootConfig.FireRate = newAttackSpeed;

                Debug.Log("Player new attack speed = " + newAttackSpeed);


                break;
            case "Explosion":
                // Send explosion variable to another script
                Debug.Log("Explosion activated");
                //Add gun impact
                SetImpactType setImpact = FindObjectOfType<SetImpactType>();
                setImpact.SetImpactTypeByName("Explosive Arrow Impact");

                new ImpactTypeModifier()
                {
                    Amount = setImpact.impactTypes[1]

                }.Apply(GunSelector.ActiveGun);
                GunSelector.ActiveGun.BulletImpactEffects = new ICollisionHandler[]
                    {
                new Explode(1.5f, new AnimationCurve(
                    new Keyframe[] { 
                        new Keyframe(0, 1), 
                        new Keyframe(1, 0.5f) }), 10, 10)
                    
                    };

            break;

            case "Frost":
                // Add frost impact
                SetImpactType setImpact2 = FindObjectOfType<SetImpactType>();
                setImpact2.SetImpactTypeByName("Frost Arrow Impact");
                Debug.Log("Frost activated");
                new ImpactTypeModifier()
                {
                    Amount = setImpact2.impactTypes[2]

                }.Apply(GunSelector.ActiveGun);
                GunSelector.ActiveGun.BulletImpactEffects = new ICollisionHandler[]
                    {

                new Frost(
                    1.5f,
                    new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0.25f) }),
                    10,
                    10,
                    new AnimationCurve(new Keyframe[] {
                        new Keyframe(0, 0.25f),
                        new Keyframe(1.75f, 0.25f),
                        new Keyframe(2, 1)
                    })
                )

        };
                break;
            default:
                Debug.LogError("Invalid ability name: " + abilityName);
                break;
        }
    }


}