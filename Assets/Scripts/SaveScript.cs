using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<GameObjectData> gameObjectDataList = new List<GameObjectData>();
}

[Serializable]
public class GameObjectData
{
    public Vector3 position;
    public Quaternion rotation;
    public string prefabName;
}

public class SaveScript : MonoBehaviour
{
    public LayerMask saveableLayerMask;
    public LayerMask saveableLayerMask2;
    public LayerMask saveableLayerMask3;
    public GameObject playerPrefab;
    public GameObject bulletsPrefab;
    public GameObject enemyPrefab;
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(saveFilePath))
        {
            Debug.Log("Save file exists at: " + saveFilePath);
        }
    }

    public void SaveGame()
    {
        File.Delete(saveFilePath);
        GameData data = new GameData();

        Collider[] PlayerLayer = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask);

        foreach (Collider collider in PlayerLayer)
        {
            UnityEngine.GameObject saveableObject = collider.gameObject;
            GameObjectData gameObjectData = new GameObjectData();
            gameObjectData.position = saveableObject.transform.position;
            gameObjectData.rotation = saveableObject.transform.rotation;
            gameObjectData.prefabName = saveableObject.name;
            data.gameObjectDataList.Add(gameObjectData);
        }

        Collider[] bulletsLayer = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask2);
        foreach (Collider collider in bulletsLayer)
        {
            UnityEngine.GameObject saveableObject = collider.gameObject;
            GameObjectData gameObjectData = new GameObjectData();
            gameObjectData.position = saveableObject.transform.position;
            gameObjectData.rotation = saveableObject.transform.rotation;
            gameObjectData.prefabName = saveableObject.name;
            data.gameObjectDataList.Add(gameObjectData);
        }
        Collider[] enemyLayer = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask3);
        foreach (Collider collider in enemyLayer)
        {
            UnityEngine.GameObject saveableObject = collider.gameObject;
            GameObjectData gameObjectData = new GameObjectData();
            gameObjectData.position = saveableObject.transform.position;
            gameObjectData.rotation = saveableObject.transform.rotation;
            gameObjectData.prefabName = saveableObject.name;
            data.gameObjectDataList.Add(gameObjectData);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            Collider[] allObjectsInLandLayer = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask);
            foreach (Collider collider in allObjectsInLandLayer)
            {
                Destroy(collider.gameObject);
            }

            Collider[] allObjectsInLandLayer2 = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask2);
            foreach (Collider collider in allObjectsInLandLayer2)
            {
                Destroy(collider.gameObject);
            }
            Collider[] allObjectsInLandLayer3 = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, saveableLayerMask3);
            foreach (Collider collider in allObjectsInLandLayer3)
            {
                Destroy(collider.gameObject);
            }

            foreach (GameObjectData gameObjectData in data.gameObjectDataList)
            {
                GameObject prefabToInstantiate = null;

                if (gameObjectData.prefabName.Contains(playerPrefab.name))
                {
                    prefabToInstantiate = playerPrefab;
                }
                else if (gameObjectData.prefabName.Contains(bulletsPrefab.name))
                {
                    prefabToInstantiate = bulletsPrefab;
                }
                else if (gameObjectData.prefabName.Contains(enemyPrefab.name))
                {
                    prefabToInstantiate = enemyPrefab;
                }
                if (prefabToInstantiate != null)
                {
                    GameObject instantiatedObject = Instantiate(prefabToInstantiate, gameObjectData.position, gameObjectData.rotation);
                    instantiatedObject.name = gameObjectData.prefabName;
                }
            }
        }
        else
        {
            Debug.Log("Save file does not exist.");
        }
    }
}