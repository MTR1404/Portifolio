using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tree, loadingImg;
    [SerializeField] GameObject[] apples;

    bool canClick;

    public int fruitCount, fruitPerClick;
    public int isAutomated;
    public float clickGap;

    private void Awake()
    { 
        fruitPerClick = PlayerPrefs.GetInt("FruitP/C", 1);
        fruitCount = PlayerPrefs.GetInt("TotalFruits", 0);
        clickGap =  PlayerPrefs.GetFloat("ClickGap", 5f);
        isAutomated = PlayerPrefs.GetInt("isAutomated", 0);
        canClick = true;


        if (isAutomated == 1)
        {
            int timeElapsed = OfflineTime.instance.timeBetweenCloseAndOpen;
            fruitCount += timeElapsed * (fruitPerClick / 10);
            StartCoroutine(StartAutomation());
        }
    }
    void Update()
    {
        ClickToIncrease();
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu");
    }

    void ClickToIncrease()
    {
        if (Input.GetMouseButtonDown(0) && canClick == true)
        {
            Vector2 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(click, Vector2.zero);

            if (hit == tree)
            {
                tree.GetComponent<Animator>().SetTrigger("Click");
                fruitCount += fruitPerClick;
                StartCoroutine(SpawnApples());
                canClick = false;
                loadingImg.SetActive(true);
                StartCoroutine(CanClick());
            }
        }
        PlayerPrefs.SetInt("FruitP/C", fruitPerClick);
        PlayerPrefs.SetInt("TotalFruits", fruitCount);
    }

    public void StartAutomating()
    {
        isAutomated = 1;
        PlayerPrefs.SetInt("isAutomated", isAutomated);
        StartCoroutine(StartAutomation());
    }

    IEnumerator SpawnApples()
    {
            for (int i = 1; i <= fruitPerClick; i++)
            {
                Instantiate(apples[Random.Range(0, apples.Length)], new Vector2(Random.Range(-8f, 8f), 4f), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(.1f, .8f));
            }
    }
    IEnumerator CanClick()
    {
        yield return new WaitForSeconds(clickGap);
        loadingImg.SetActive(false);
        canClick = true;
    }

    IEnumerator StartAutomation()
    {
        while (true)
        {
            fruitCount += (fruitPerClick / 10).ConvertTo<int>();
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
        //StopAllCoroutines();
    }
}
