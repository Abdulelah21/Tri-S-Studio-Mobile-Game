using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.TakeDamage(dmg);
        Debug.Log(GameManager.gameManager._playerHealth.Health);
    }
    public void PlayerGetHeal(int healing)
    {
        GameManager.gameManager._playerHealth.TakeHeal(healing);
    }
}
