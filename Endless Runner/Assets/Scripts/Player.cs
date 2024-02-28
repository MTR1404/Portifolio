using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] int jumpForce;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = .15f;

    Animator anim;
    Rigidbody2D rb;
    Transform player;
    UIManager manager;
    bool isGrounded = true;
    bool canRun = false;
    float canFire = -0.5f;
    int moveSpeed = 2;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        manager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    void Update()
    {
        Movement();
    }

    public void StartRunning()
    {
        canRun = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) isGrounded = false;
        else isGrounded = true;
    }

    void Movement()
    {
        if (canRun == true)
        {
            anim.SetBool("isRunning", true);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
                isGrounded = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                canFire = Time.time + fireRate;
                anim.SetBool("isShooting", true);
                Instantiate(bullet, shootPos.position, Quaternion.identity);
                StartCoroutine(ShootAnim());

            }
            if (transform.position.x != -4) moveSpeed = 2;
            else moveSpeed = 0;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            ResetPos();
        }
        
    }
    void Jump()
    {
        StartCoroutine(JumpAnim());
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void ResetPos()
    {
        if (transform.position.x > -4) transform.position = new Vector3(-4, transform.position.y, 0);
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-4f, -1.5f, 0F);
            manager.OffScreenHealth();
        }
    }

    IEnumerator JumpAnim()
    {
        anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("isJumping", false);
    }

    IEnumerator ShootAnim()
    {
        yield return new WaitForSeconds(.2f);
        anim.SetBool("isShooting", false);
    }
}
