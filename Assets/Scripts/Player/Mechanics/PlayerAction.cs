using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private PlayerGunSelector GunSelector;
    private PlayerTouchMovement player;
    private void Start()
    {
        player = GetComponent<PlayerTouchMovement>();
    }
    private void Update()
    {
        if(!player.IsMoving && GunSelector.ActiveGun != null)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }
}
