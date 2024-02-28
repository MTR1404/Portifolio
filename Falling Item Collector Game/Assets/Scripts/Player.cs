using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] bool gameOver;
    [SerializeField] UIController UI;
    [SerializeField] AudioClip fruitClip, collectibleClip;
    Animator anim;
    AudioSource speaker;

    void Update()
    {
        if(!gameOver) Movement();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x < -31) transform.position = new Vector3(-31f, transform.position.y, 0);
        if (transform.position.x > 31) transform.position = new Vector3(31f, transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit" )
        { 
            speaker.clip = fruitClip;
            speaker.Play();
            Destroy(collision.gameObject);
            UI.UpdateScore();
        }

        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Speed()
    {
        speaker.clip = collectibleClip;
        speaker.Play();
        StartCoroutine(SpeedPowerUP());
    }

    public void Range()
    {
        speaker.clip = collectibleClip;
        speaker.Play();
        StartCoroutine(RangePowerUp());
    }


    IEnumerator SpeedPowerUP()
    {
        speed *= 3;
        yield return new WaitForSeconds(5);
        speed /= 3;
    }

    IEnumerator RangePowerUp()
    {
        anim.Play("Grow");
        yield return new WaitForSeconds(5);
        anim.Play("Normal");
    }

     public void GameOver()
    {
        gameOver = true;
    }
}
