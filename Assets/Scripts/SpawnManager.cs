using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnManager : MonoBehaviour
{
    GameObject player;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = false;

    float distanceSpawnFromHero;

    List<GameObject> pooledObjects;

    void Start()
    {
        GetPlayerInformation();
        SpawnEnemies();

        InvokeRepeating("EnemyActivated", 1f, 1f);

    }

    void GetPlayerInformation()
    {
        player = GameObject.Find("Hero");
        distanceSpawnFromHero = player.GetComponentInChildren<SphereSpawnScript>().GetDistanceSpawn();
    }

    void SpawnEnemies()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    void EnemyActivated()
    {
        GameObject enemy = GetPooledObject();

        if (enemy != null)
        {
            enemy.transform.position =
                player.transform.position +
                DegreeToVector3(Random.Range(0, 360)) * distanceSpawnFromHero;

            enemy.SetActive(true);
        }
        else
        {
            CancelInvoke("EnemyActivated");
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

/*        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
*/
        return null;
    }

    public static Vector3 DegreeToVector3(float degree)
    {
        float radAngle = Mathf.Deg2Rad * degree;

        return new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle));
    }

    private void OnEnable()
    {
        Health.Died += Respawn;
    }

    private void OnDisable()
    {
        Health.Died -= Respawn;
    }

    void Respawn(GameObject enemy)
    {
        GameObject enemy1 = GetPooledObject();

        if (enemy != null)
        {
            enemy.transform.position =
                player.transform.position +
                DegreeToVector3(Random.Range(0, 360)) * distanceSpawnFromHero;

            enemy.SetActive(true);
        }
    }
}

