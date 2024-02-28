using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Fade;
    [SerializeField] Text bestScoreTxt;
    AudioSource press;

    private void Start()
    {
        press = GetComponent<AudioSource>();

        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScoreTxt.text = "High Score: " + bestScore;
    }
    public void Play()
    {
        press.Play();
        Fade.SetActive(true);
        
        StartCoroutine(GoToGame());
    }

    public void Exit()
    {
        press.Play();
        Application.Quit();
    }

    IEnumerator GoToGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
      
    }
}
