using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool canhit = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canhit)
        {
            canhit = false;
            Debug.Log("Hit: " + collision.name);
            IDamagable hit = collision.GetComponentInChildren<IDamagable>();
            if (hit != null) hit.Damage();
            StartCoroutine(HitSpacing());
        }
    }

    IEnumerator HitSpacing()
    {
        yield return new WaitForSeconds(1f);
        canhit = true;
    }
}
