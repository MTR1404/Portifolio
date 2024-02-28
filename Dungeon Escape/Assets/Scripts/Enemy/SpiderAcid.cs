using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAcid : MonoBehaviour
{
    [SerializeField] int speed;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamagable hit = collision.GetComponent<IDamagable>();
            hit?.Damage();
            Destroy(gameObject);
        }
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }   
}
