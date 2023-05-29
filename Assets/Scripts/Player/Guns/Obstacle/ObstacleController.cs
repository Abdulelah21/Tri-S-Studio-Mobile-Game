using UnityEngine;

public class ObstacleController : MonoBehaviour
{

        private Rigidbody rb;
    PlayerTouchMovement player;
        void Start()
    {
        player = GetComponent<PlayerTouchMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!player.IsMoving) { 
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