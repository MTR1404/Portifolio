using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopEnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject projectile, explosion;
    [SerializeField] Transform shootTransform;

    UIManager manager;
    void Start()
    {
        StartCoroutine(Behaviour());
        manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * 3 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            manager.OffScreenHealth();
            Destroy(gameObject);
        }
    }

    IEnumerator Behaviour()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Instantiate(projectile, shootTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(.4f, .8f));
            Instantiate(projectile, shootTransform.position, Quaternion.identity);
        }
    }
}
