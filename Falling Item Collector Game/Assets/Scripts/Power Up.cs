using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] int ID;

    Player player;
    UIController controller;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        controller = GameObject.Find ("UI Controller").GetComponent<UIController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                switch (ID)
                {
                    case 0: player.Speed(); break;

                    case 1: player.Range(); break;

                    case 2: controller.UpdateLives(); break;
                }
            }
        }
    }
}
