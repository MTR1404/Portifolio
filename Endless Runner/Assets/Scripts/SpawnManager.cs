using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject ground, groundCluster;
    [SerializeField] Sprite[] bgShops;
    [SerializeField] List<GameObject> vehicles = new List<GameObject>();
    [SerializeField] List<GameObject> runningEnemies = new List<GameObject>();
    [SerializeField] List<GameObject> flyingEnemies = new List<GameObject>();

    List<GameObject> shops = new List<GameObject>();
    
    bool canSpawn = false;
    void Start()
    {
        StartCoroutine(SpawnBGAsset());
        StartCoroutine(SpawnVehicles());
        StartCoroutine(SpawnFlyingEnemies()); 
        StartCoroutine(SpawnRunningEnemies());
        StartCoroutine (SpawnGrounds());
    }

    public void StartSpawn()
    {
        canSpawn = true;
    }

    void CreateBGObject()
    {
        if (canSpawn)
        {
            GameObject shop = new GameObject("Shop");
            shop.AddComponent<SpriteRenderer>().sprite = bgShops[Random.Range(0, bgShops.Length)];
            shop.transform.position = new Vector3(13f, -2.5f, 0);
            shop.transform.localScale = Vector3.one * 5;
            shop.AddComponent<ShopMovement>();
            shops.Add(shop);
        }
    }

    void SpawnVehicle()
    {
        if (canSpawn)
        {
            GameObject Vehicle = Instantiate(vehicles[Random.Range(0, vehicles.Count)], new Vector3(13f, -2.1f, 0), Quaternion.identity);
            if (Vehicle.tag == "Truck") Vehicle.transform.position = new Vector3(-13f, -.9f, 0);
        }
    }

    void SpawnRunningEnemy()
    {
        if (canSpawn)
        {
            GameObject enemy = Instantiate(runningEnemies[Random.Range(0, runningEnemies.Count)], new Vector3(11f, -2.5f, 0), Quaternion.identity);
        }
    }
    void SpawnFlyingEnemy()
    {
        if (canSpawn)
        {
            GameObject enemy = Instantiate(flyingEnemies[Random.Range(0, flyingEnemies.Count)], new Vector3(11f, .4f, 0), Quaternion.identity);
        }
    }

    void SpawnGround()
    {
        if (canSpawn)
        {
           Ground [] clusters = groundCluster.GetComponentsInChildren<Ground>();
            foreach (Ground g in clusters) g.enabled = true;
            Instantiate(ground, new Vector3(14.78f, -4.16f, 0), Quaternion.identity);
        }
    }
    IEnumerator SpawnBGAsset()
    {
        while (true)
        {
            CreateBGObject();
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    IEnumerator SpawnVehicles()
    {
        while (true)
        {
            SpawnVehicle();
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    IEnumerator SpawnRunningEnemies()
    {
        while (true)
        {
            SpawnRunningEnemy();
            yield return new WaitForSeconds(Random.Range(8f, 12f));
        }
    }

    IEnumerator SpawnFlyingEnemies()
    {
        while (true)
        {
            SpawnFlyingEnemy();
            yield return new WaitForSeconds(Random.Range(6f, 15f));
        }
    }

    IEnumerator SpawnGrounds()
    {
        while (true)
        {
            SpawnGround();
            yield return new WaitForSeconds(1f);
        }
    }
}
