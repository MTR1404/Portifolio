using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroCanvas());
    }


    IEnumerator DestroCanvas()
    {
        Destroy(this.gameObject);
        yield return new WaitForSeconds(2);
    }
}
