using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.left * Random.Range(5f, 10f) * Time.deltaTime);
    }
}
