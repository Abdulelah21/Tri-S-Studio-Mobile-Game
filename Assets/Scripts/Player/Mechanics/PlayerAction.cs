using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private PlayerGunSelector GunSelector;
    private PlayerTouchMovement player;
    public int NumberOfEnemies = 0;
    public bool IsPlayerLooking = false;
    public PlayerLook playerEyes;


    private void Start()
    {
        player = GetComponent<PlayerTouchMovement>();
    }
    private void Update()
    {

        if (!player.IsMoving && GunSelector.ActiveGun != null && NumberOfEnemies != 0 && IsPlayerLooking)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }

    public int GetNumbers(int enemies)
    {
        NumberOfEnemies = enemies;
        return NumberOfEnemies;

    }


}


