using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;
    public float bulletSpeed = 10f;
    public float stoppingDistance = 10f;
    public float retreatDistance = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 1f;
    public float range = 100f;
    public GameObject bulletDecalPrefab;

    private Transform player;
    private float nextFireTime = 0f;
    private Vector3 targetPosition;
    private float searchTimer = 5f;
    private float currentTimer;
    public bool seePlayer = false;
    private float nextRaycastTime = 0f; // Initialize to 0
    private float raycastInterval = 1f; // Interval between each Raycast. Adjust as needed.
    private float moveSpeed = 5f; // Adjust as needed    private AudioSource audioSource;
    private AudioSource audioSource;
    private Transform gun;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = GetRandomPosition();
        currentTimer = searchTimer;
        audioSource = GetComponent<AudioSource>();
        gun = transform.GetChild(0);
    }

    private float fieldOfView = 90f; // Field of view in degrees

    private void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float raycastDistance = 100f;

        Vector3 raycastStartPoint = transform.position;

        Debug.DrawRay(raycastStartPoint, directionToPlayer * raycastDistance, Color.red);

        if (Time.time >= nextRaycastTime)
        {
            if (Physics.Raycast(raycastStartPoint, directionToPlayer, out RaycastHit hit, raycastDistance))
            {
                if (hit.transform == player)
                {
                    // Check if player is within field of view
                    if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfView / 2)
                    {
                        seePlayer = true;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), 1f);
                        gun.rotation = Quaternion.Slerp(gun.rotation, Quaternion.LookRotation(directionToPlayer), 1f); // Rotate the gun towards the player

                        if (Time.time >= nextFireTime)
                        {
                            audioSource.Play();
                            nextFireTime = Time.time + 1f / fireRate;
                            Fire();
                        }
                    }
                    // Removed else clause
                }
                else
                {
                    seePlayer = false;
                }
            }
            else
            {
                seePlayer = false;
            }

            nextRaycastTime = Time.time + raycastInterval;
        }

        if (!seePlayer)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= searchTimer)
            {
                targetPosition = GetRandomPosition();
                currentTimer = 0f;
            }

            MoveTowardsTarget(); // Call MoveTowardsTarget when the enemy does not see the player
        }
        else
        {
            MoveTowardsTarget(); // Call MoveTowardsTarget when the enemy sees the player
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        transform.position += directionToTarget * moveSpeed * Time.deltaTime;
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);

        return new Vector3(x, transform.position.y, z);
    }

    private void Fire()
    {
        if (player == null) return;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Vector3 direction = (player.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * range;
    }
    IEnumerator TurnToPlayer()
    {
        Quaternion startRotation = transform.rotation;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion endRotation = Quaternion.LookRotation(directionToPlayer);
        float elapsedTime = 0;
        float turnDuration = 0.4f; // Duration of the turn in seconds

        while (elapsedTime < turnDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 20;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                // Turn towards the player when hit
                StartCoroutine(TurnToPlayer());
            }
        }
    }
    private void Die()
    {
        gameObject.tag = "Dead";
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        DieManager.DieEnemy(gameObject);
        gameObject.GetComponent<Enemy>().enabled = false;

    }

}