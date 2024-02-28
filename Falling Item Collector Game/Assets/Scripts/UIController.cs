using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject pauseMenu, pauseBtn, playBtn, fade;
    [SerializeField] Text endtxt;
    [SerializeField] AudioSource speaker;

    int lives = 3;
    int score;
    int bestScore;

    private void Update()
    {
        scoreText.text = "Score: " + score;

        if (Input.GetKeyDown(KeyCode.Escape) && pauseBtn.activeSelf) Pause();
        if (lives < 1) GameOver();
    }
    public void UpdateScore()
    {
        score++;
    }

    public void UpdateLives()
    {
        GetComponent<AudioSource>().Play();   
        lives--;
        hearts[lives].SetActive(false);
    }

    public void Pause()
    {
        speaker.Play();
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        
    }

    public void Continue()
    {
        speaker.Play();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        speaker.Play();
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(1));
    }

    public void Home()
    {
        speaker.Play();
        Time.timeScale = 1f;
        StartCoroutine(LoadScene(0));
    }

    public void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
        }
    }

    void GameOver()
    {
        GameObject [] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        Player player = GameObject.Find("Player").GetComponent<Player>();

        foreach (GameObject fruit in fruits) Destroy(fruit);
        foreach (GameObject collectible in collectibles) Destroy(collectible);

        player.GameOver();
        pauseMenu.SetActive(true);
        playBtn.SetActive(false);
        Destroy(fade);
        endtxt.gameObject.SetActive(true);
        UpdateBestScore();
        endtxt.text = "You died. \n Your score is " + score + ".";
    }

    IEnumerator LoadScene(int scene)
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(scene);
    }

}
