using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacel : MonoBehaviour
{
    public int minDamage = 30;
    public int maxDamage = 45;
    public int speed = 20;
    private Rigidbody rb;
    private Vector3 storeBoundry;

/*    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed, 0, speed);
        storeBoundry = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }*/
    int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

/*    private void Update()
    {
        if (transform.position.x > storeBoundry.x * -2 || transform.position.z > storeBoundry.z * -2)
        {
            Destroy(this.gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") || other.CompareTag("wall"))
        {
            int damage = GetRandomDamage();
            Destroy(gameObject);
            Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
            Debug.Log("Enemy got "+GetRandomDamage()+" Damage");
            enemyHealth.TakeDamage(damage);
        }
    }
}
