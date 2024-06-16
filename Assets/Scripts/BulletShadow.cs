using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShadow : MonoBehaviour
{
    private void Start()
    {
        if (transform.parent != null) // Check if the parent exists
        {
            if (transform.parent.tag != "Enemy" && transform.parent.tag != "Player") // Replace "SomeTag" with the tag you want to check
            {
                StartCoroutine(DestroyBullet());
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}