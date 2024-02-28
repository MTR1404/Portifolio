using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] int speed;
    UIManager manager;
    void Start()
    {
        manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            manager.UpdateHealth();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
