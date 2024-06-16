using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawenemies : MonoBehaviour
{
    public GameObject enemyPrefab; // Посилання на префаб ворога
    public int maxEnemies = 3; // Максимальна кількість ворогів на сцені

    void Update()
    {
        // Знаходимо всіх ворогів на сцені
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Якщо ворогів менше, ніж maxEnemies, спавнимо нового ворога
        if (enemies.Length < maxEnemies)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
            PlayerPrefs.Save();
        }
    }
}