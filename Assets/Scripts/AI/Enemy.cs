using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public float moveSpeed = 2.0f;
    public float damage = 1.0f;
    public float attackRange = 1.5f;

    private int currentHealth;
    private GameObject player;
    private Vector3 targetPosition;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = transform.position;
    }

    void Update()
    {
        // Move towards player if within attack range
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            targetPosition = player.transform.position;
        }

        // Move towards target position
        Vector3 movementDirection = (targetPosition - transform.position).normalized;
        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy killed");
        // TODO: Handle enemy death
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage player
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.TakeDamage(1);
        }
    }
}
