using System.Collections;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float levitationAmplitude = 0.5f;
    public float levitationFrequency = 1f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(LevitateAndRotate());
    }

    IEnumerator LevitateAndRotate()
    {
        while (true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * levitationFrequency) * levitationAmplitude, 0);

            yield return null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().guns[0].activeSelf == true)
            {
                PlayerPrefs.SetInt("AllAmmo", PlayerPrefs.GetInt("AllAmmo") + 20);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("AllAmmo", PlayerPrefs.GetInt("AllAmmo") + 10);
                PlayerPrefs.Save();
            }
            Destroy(gameObject);
        }
    }
}