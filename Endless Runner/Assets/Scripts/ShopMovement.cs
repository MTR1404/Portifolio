using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.left * 5 * Time.deltaTime);
        if (transform.position.x < -13f) Destroy(gameObject);
    }
}
