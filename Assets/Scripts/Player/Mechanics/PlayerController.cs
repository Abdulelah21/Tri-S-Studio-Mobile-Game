using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;
    public float shootRange = 10.0f;
    public float shootInterval = 0.5f;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public GameObject bullet;
    Enemy enemy;
    private Rigidbody rb;
    private Vector3 moveInput;
    private Camera mainCamera;
    private Quaternion targetRotation;
    private float lastShootTime = 0.0f;
    public int maxHealth = 10;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        targetRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        moveInput = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Move player using Rigidbody
        rb.velocity = moveInput * moveSpeed;

        // Rotate player to face movement direction
        if (moveInput != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(moveInput);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);

    }
    public void TakeDamage(int damage)
    {
        // Reduce player health
        currentHealth -= damage;

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Disable player control
        GetComponent<PlayerController>().enabled = false;

        // Trigger death animation or game over screen
        // ...

        // For demo purposes, destroy the player object after a short delay
        Destroy(gameObject, 2.0f);
    }

    void Shoot()
    {
        // Spawn bullet at shoot point        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Rotate bullet to face shooting direction
        bullet.transform.rotation = Quaternion.LookRotation(transform.forward);

        // Check if bullet hits enemy
        RaycastHit hit;

        if (Physics.Raycast(shootPoint.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("enemy"))
            {
                // Damage enemy
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
            }
        }
    }

    void LateUpdate()
    {
        // Set camera rotation to initial rotation
        mainCamera.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            // Damage player
            enemy.TakeDamage(1);
        }
    }

    void Update()
    {
        /*        // Check if player is shooting
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }*/

        // Check if player is not moving
        if (moveInput == Vector3.zero)
        {
            // Check for enemies within shoot range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootRange);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("enemy"))
                {
                    // Check if enough time has passed since last shoot
                    if (Time.time > lastShootTime + shootInterval)
                    {
                        // Rotate player to face enemy
                        targetRotation = Quaternion.LookRotation(hitCollider.transform.position - transform.position);

                        // Shoot at enemy
                        Shoot();

                        // Reset last shoot time
                        lastShootTime = Time.time;
                    }
                }
            }
        }
    }
}

                
            
        
    



