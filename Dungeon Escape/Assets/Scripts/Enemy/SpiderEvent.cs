using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderEvent : MonoBehaviour
{
    [SerializeField] UnityEvent fire;
    public void Fire()
    {
        fire?.Invoke();
    }
}
