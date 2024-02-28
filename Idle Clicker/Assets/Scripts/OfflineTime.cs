using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineTime : MonoBehaviour
{
    public int timeBetweenCloseAndOpen;

    public static OfflineTime instance;

    private void Awake()
    {
        instance = this;
        DateTime now = DateTime.Now;

        if (PlayerPrefs.HasKey("Save"))
        {
            string timeString = PlayerPrefs.GetString("Save");
            DateTime exitTime = DateTime.Parse(timeString);
            TimeSpan timeDifference = now - exitTime;
            timeBetweenCloseAndOpen = (int)timeDifference.TotalSeconds;
            Debug.Log($"Time passed: {timeBetweenCloseAndOpen} s");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Save", DateTime.Now.ToString());
    }
}
