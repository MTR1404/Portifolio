using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int health = 100;
    float score = 0;
    bool canStart = false;
    int bestscore;
    AudioSource speaker, beep;

    public UnityEvent OnWpressed;

    [SerializeField] AudioClip start;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Player player;
    [SerializeField] Text scoretxt, healthtxt, finaltxt, firstTxt, countdown;
    [SerializeField] GameObject pauseMenu, pauseBtn, playBtn, fade;
    void Start()
    {
        bestscore = PlayerPrefs.GetInt("HighScore", 0);
        speaker = GetComponent<AudioSource>();
        beep = spawnManager.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W)) StartCoroutine(StartGears());

        if (canStart)
        {
            score += Time.deltaTime * 10;
            healthtxt.text = health.ToString();
            scoretxt.text = "Score: " + score.ConvertTo<int>();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Time.timeScale == 0) Play();
                else Pause();
            }
        }

        if (health < 1)
        {
            health = 0;
            canStart = false;
            Pause();
            UpdateHighScore();
            playBtn.SetActive(false);
            pauseBtn.SetActive(false);
            finaltxt.enabled = true;
            finaltxt.text = "You have been decomissioned. \n Your Score was " + score;
        }
    }
    public void UpdateScore()
    {
        score += 100;
    }

    public void OffScreenHealth()
    {
        if (health > 5) health -= health/3;
        else health--;
    }

    public void UpdateHealth()
    {
        if (health > 5) health -= Random.Range(2, 5);
        else health--;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        speaker.Play();
        pauseBtn.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Play()
    {
        Time.timeScale = 1;
        speaker.Play();
        pauseBtn.SetActive(true);   
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        speaker.Play();
        StartCoroutine(LoadScene(1));
    }

    public void Home()
    {
        speaker.Play();
        StartCoroutine(LoadScene(0));
    }

    void UpdateHighScore()
    {
        if (score > bestscore)
        {
            bestscore = score.ConvertTo<int>();
            PlayerPrefs.SetInt("HighScore", bestscore);
        }
    }

    IEnumerator LoadScene(int scene)
    {
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public IEnumerator StartGears()
    {
        Destroy(firstTxt.gameObject);
        countdown.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            if (i == 1) beep.clip = start;
            beep.Play();
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        Destroy(fade);
        Destroy(countdown.gameObject);
        OnWpressed?.Invoke();
        pauseBtn.SetActive(true);
        canStart = true;
    }
}
