using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollingBackgrounds : MonoBehaviour
{
    [SerializeField] float speed, changeTimer;
    [SerializeField] Texture [] bgs;

    Renderer bgRenderer;
    bool gameStarted = false;
    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
        StartCoroutine(ChangePicture());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && gameStarted != true )
        {
            StartCoroutine(StartMoving());
        }

        MoveBackground();

    }

    IEnumerator ChangePicture()
    {
        while (true)
        {
            bgRenderer.material.mainTexture = bgs[Random.Range(0, bgs.Length)];
            yield return new WaitForSeconds(changeTimer);
        }
    }

    void MoveBackground()
    {
        if (gameStarted) bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(3.1f);
        gameStarted = true;
    }
}
