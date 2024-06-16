using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletDecalPrefab; // Посилання на префаб сліду від кулі

    private void OnCollisionEnter(Collision other)
    {
        // Створюємо слід від кулі перед її знищенням
        GameObject decal = Instantiate(bulletDecalPrefab, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
        decal.transform.SetParent(other.transform);

        // Зупиняємо кулю перед її знищенням
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Destroy(gameObject);
    }
}