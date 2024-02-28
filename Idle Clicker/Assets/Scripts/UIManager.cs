using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text pickings, speed, gap, info;
    [SerializeField] Animator txt_anim, time_decreased_anim, automate_txt;
    [SerializeField] GameManager _gameManager;

    [SerializeField] GameObject timeShaveBtn, automateBtn;

    int shaveTimeCost;

    private void Awake()
    {
        shaveTimeCost = PlayerPrefs.GetInt("ShaveTimeCost", 1000);

        if (_gameManager.isAutomated == 1) automateBtn.SetActive(false);
    }
    private void Update()
    {
        TextManagement();
    }
    public void AddPickingLeft( int pickingIncrease)
    {
        int cost = (pickingIncrease * .8).ConvertTo<int>();

        if (_gameManager.fruitCount >= cost)
        {
            _gameManager.fruitCount -= cost;
            _gameManager.fruitPerClick += pickingIncrease;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            txt_anim.SetTrigger("Click");
        }
    }
    public void AddPickingRight(int pickingIncrease)
    {
        int cost = (pickingIncrease * .9).ConvertTo<int>();

        if (_gameManager.fruitCount >= cost)
        {
            _gameManager.fruitCount -= cost;
            _gameManager.fruitPerClick += pickingIncrease;
            GetComponent<AudioSource>().Play();
        }

        else
        {
            txt_anim.SetTrigger("Click");
        }
    }

    void TextManagement()
    {
        pickings.text = _gameManager.fruitCount.ToString() + " Pickings";
        speed.text = _gameManager.fruitPerClick.ToString() + " Pickings / Click";
        info.text = "Automation Cost : 10,000 \n Shaving Time Cost : " + shaveTimeCost;
        gap.text = "Time Between Clicks: " + (Mathf.CeilToInt(_gameManager.clickGap * Mathf.Pow(10, 2)) / Mathf.Pow(10, 2)).ToString() + " sec";
    }

    public void Automate(int cost)
    {
        if (_gameManager.fruitCount >= cost)
        {
            _gameManager.fruitCount -= cost;
            _gameManager.StartAutomating();
            automateBtn.gameObject.SetActive(false);
            GetComponent<AudioSource>().Play();
        }

        else
        {
            txt_anim.SetTrigger("Click");
        }
    }

    public void ShaveTime()
    {
        if (_gameManager.fruitCount >= shaveTimeCost)
        {
            if (_gameManager.clickGap < 0.01)
            {
                _gameManager.clickGap = 0;
                timeShaveBtn.gameObject.SetActive(false);
                GetComponent<AudioSource>().Play();
            }

            else
            {
                _gameManager.fruitCount -= shaveTimeCost;
                _gameManager.clickGap *= 0.95f;
                shaveTimeCost = (shaveTimeCost * 2.25f).ConvertTo<int>();
                PlayerPrefs.SetInt("ShaveTimeCost", shaveTimeCost);
                PlayerPrefs.SetFloat("ClickGap", _gameManager.clickGap);
                time_decreased_anim.SetTrigger("Click");
                GetComponent<AudioSource>().Play();
            }
        }

        else
        {
            txt_anim.SetTrigger("Click");
        }
    }

    public void Infobutton()
    {
        if (info.gameObject.activeSelf) info.gameObject.SetActive(false);

        else
        {
            info.gameObject.SetActive(true);
        }
    }
}
