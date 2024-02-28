using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunkEnemyBehaviour : MonoBehaviour
{
    [SerializeField] Sprite shoot;
    [SerializeField] Transform shootTransform;
    [SerializeField] GameObject projectile, explosion;
    [SerializeField] int speed;

    Animator anim;
    UIManager manager;
    void Start()
    {
        anim = GetComponent<Animator>();
        manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        StartCoroutine(Behaviour());
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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
            Stop();
            yield return new WaitForSeconds(.4f);
            Run();
        }
    }

    void Stop()
    {
        speed = 0;
        Instantiate(projectile, shootTransform.position, Quaternion.identity);
        anim.SetBool("isRunning", false);
    }

    void Run()
    {
        anim.SetBool("isRunning", true);
        speed = 3;
    }
}
