using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private Rigidbody rb;
    PlayerShooting playerIsShooting;
    void Start()
    {
        playerIsShooting = FindObjectOfType<PlayerShooting>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool playIsmoving = playerIsShooting.CheckPlayerIsMoving();
        if (playIsmoving) { 
            Debug.Log("Player Is Moving");
            rb.isKinematic = true;
        }
        else
        {
            Debug.Log("Player Is Not Moving");
            rb.isKinematic = false;

        }
    }


}