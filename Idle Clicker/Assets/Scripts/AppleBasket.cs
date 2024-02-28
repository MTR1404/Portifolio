using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBasket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>().Play();
        Destroy(collision.gameObject);
    }
}
