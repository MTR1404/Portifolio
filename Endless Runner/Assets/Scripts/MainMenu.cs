using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text bestScore;
    [SerializeField] GameObject fade;

    AudioSource speaker;

    private void Start()
    {
        bestScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        speaker = GetComponent<AudioSource>();
    }
    public void Play()
    {
        speaker.Play();
        fade.SetActive(true);
        StartCoroutine(LoadGame());
    }

    public void Exit()
    {
        speaker.Play();
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
