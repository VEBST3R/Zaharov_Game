using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManager : MonoBehaviour
{
    public static DieManager instance;
    public UI_Script uiScript;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DieEnemy(GameObject enemy)
    {
        instance.StartCoroutine(instance.DestroyEnemy(enemy));
    }

    IEnumerator DestroyEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(5f);
        Destroy(enemy);

    }
    public static void DiePlayer(GameObject player)
    {
        instance.StartCoroutine(instance.DestroyPlayer(player));

    }

    IEnumerator DestroyPlayer(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        uiScript.EndGame();
    }

}