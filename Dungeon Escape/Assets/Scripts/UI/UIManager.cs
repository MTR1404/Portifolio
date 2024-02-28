using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager Instance;
    public static UIManager instance
    {
        get
        {
            if (Instance == null) Debug.Log("UI Manager: 404");
            return Instance;    
        }
    }

    [SerializeField] Text playerGemCount;
    [SerializeField] Image selection;

    public void OpenShop(int gems)
    {
        playerGemCount.text = gems.ToString() + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selection.rectTransform.anchoredPosition = new Vector2(selection.rectTransform.anchoredPosition.x, yPos);
    }
    private void Awake()
    {
        Instance = this;
    }

}
