using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] GameObject explosion1, explosion2, explosion3;

    UIManager manager;
    void Start()
    {
        manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cop Enemy")
        {
            Instantiate(explosion1, collision.transform.position, Quaternion.identity);
            manager.UpdateScore();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Punk Enemy")
        {
            Instantiate(explosion2, collision.transform.position, Quaternion.identity);
            manager.UpdateScore();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag != "Player")
        {
            if (collision.gameObject.tag == "Enemy Projectile")
            {
                //Instantiate(explosion3, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
