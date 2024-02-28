using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{ 
    [SerializeField] Sprite[] fruits;
    [SerializeField] GameObject[] collectibles;
    [SerializeField] GameObject livesEnder, pauseBtn, fade;
    [SerializeField] Text startTxt;
    
    float gravityScale = .3f;
    float spawnMaxTimer = 1.5f;

    void Update()
    {
        if (startTxt.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            startTxt.gameObject.SetActive(false);
            pauseBtn.SetActive(true);
            Destroy(fade);
            StartCoroutine(SpawnFruits());
            StartCoroutine(SpawnCollectibles());
            StartCoroutine(FruitSpawnSpeedUp());
            StartCoroutine(SpawnLivesEnder());
            StartCoroutine(EnemySpawnSpeedUP());
        }
    }

    IEnumerator SpawnFruits()
    {
        while (true)
        {
            SpawnItem();
            float spawnTime = Random.Range(0, 1.5f);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator SpawnCollectibles()
    {
        while (true)
        {
            SpawnCollectible();
            float spawnTime = Random.Range(6f, 8f);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator SpawnLivesEnder()
    {
        while (true)
        {
            SpawnLivesEnders();
            float spawnTime = Random.Range(0f, spawnMaxTimer);
            yield return new WaitForSeconds(spawnTime);
        }
    }


    void SpawnItem()
    {
        int chooseItem = Random.Range(0, fruits.Length);
        GameObject item = new GameObject("Fruit");
        
        SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = fruits[chooseItem];
        spriteRenderer.sortingOrder = 1;
        
        Collider2D col = item.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        Rigidbody2D rb = item.AddComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.mass = Random.Range(.9f, 1f);

        item.tag = "Fruit";
        item.transform.localScale = Vector3.one * 25;
        item.transform.position = new Vector3(Random.Range(-32f, 32f), 23f, 0);
    }

    void SpawnCollectible()
    {
        int chooseItem = Random.Range(0, collectibles.Length);
        Instantiate(collectibles[chooseItem], new Vector3(Random.Range(-32f, 32f), 23f, 0), Quaternion.identity);
    }

    void SpawnLivesEnders()
    {
        Instantiate(livesEnder, new Vector3(Random.Range(-32f, 32f), 23f, 0), Quaternion.identity);
    }

    IEnumerator FruitSpawnSpeedUp()
    {
        while (true)
        {
            gravityScale *= 1.2f;
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator EnemySpawnSpeedUP()
    {
        while (true)
        {
            spawnMaxTimer *= .8f;
            yield return new WaitForSeconds(10f);
        }
    }
}
