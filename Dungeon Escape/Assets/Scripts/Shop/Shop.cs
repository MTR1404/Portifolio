using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] Image selection;
    
    int buyID, itemCost;
    Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) shopPanel.SetActive(true);
        
        Player player = collision.GetComponent<Player>();
        if (player != null) UIManager.instance.OpenShop(player.diamond);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) shopPanel.SetActive(false);
    }

    public void SelectItem(int item)
    {
        // 0 = sword
        //1 = key
        //2 = keys
        switch (item) 
        {
            case 0:
                UIManager.instance.UpdateShopSelection(60);
                buyID = 0;
                itemCost = 200;
                break;

            case 1:
                UIManager.instance.UpdateShopSelection(-50);
                buyID = 1;
                itemCost = 400;
                break;

            case 2:
                UIManager.instance.UpdateShopSelection(-150);
                buyID = 2;
                itemCost = 100;
                break;

        }
    }

    public void BuyItem()
    {
        if (player.diamond >= itemCost)
        {
            player.diamond -= itemCost;
            if(buyID == 2) GameManager.Instance.hasKeysToCastle = true;
        }

        else
        {
             shopPanel.SetActive(false);
        }
    }
}
