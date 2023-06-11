using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    public BoxColliderDetection enemyNumbers;
    public RandomAbilityGenerator GenerateAbilities;
    public GameObject ShowAbilities, PlayerHUD;
    public bool buttonIsClicked = false;

    public Teleporter teleport;
    private void Awake()
    {
        GenerateAbilities.enabled = false;
        ShowAbilities.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

        teleport = FindObjectOfType<Teleporter>();
       /* canvas = GetComponent<Canvas>();*/
        enemyNumbers = FindObjectOfType<BoxColliderDetection>();
        GenerateAbilities = GetComponent<RandomAbilityGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyNumbers.CurrentNumberOfEnemies == 0 && !buttonIsClicked)
        {
            PlayerHUD.SetActive(false);
            Time.timeScale = 0f;
            ShowAbilities.SetActive(true);
            GenerateAbilities.enabled = true;

            

        }
        if (buttonIsClicked)
        {
            PlayerHUD.SetActive(true);
            Time.timeScale = 1f;

            GameObject Ability1 = GameObject.Find("Ability1(Clone)");
            Destroy(Ability1);
            GameObject Ability2 = GameObject.Find("Ability2(Clone)");
            Destroy(Ability2);
            GameObject Ability3 = GameObject.Find("Ability3(Clone)");
            Destroy(Ability3);
            ShowAbilities.SetActive(false);
            teleport.noEnemies = true;
            teleport.material.color = Color.green;
        }
    }
}
