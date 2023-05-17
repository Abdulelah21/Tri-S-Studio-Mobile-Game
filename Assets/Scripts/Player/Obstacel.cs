using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") || other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
