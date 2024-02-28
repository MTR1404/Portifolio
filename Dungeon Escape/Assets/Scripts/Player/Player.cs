using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] int jumpForce, speed;
    [SerializeField] LayerMask ground;
    [SerializeField] int health;

    public int diamond;
    
    Rigidbody2D playerRb;
    PlayerAnimation playerAnimation;
    bool resetJump = false;

    public int Health { get; set; }
    bool isDead = false;    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        Health = health;
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
            Attack();
        }
    }

    public void AddDiamond(int gems)
    {
        diamond+= gems;
    }

    public void Damage()
    {
        if (!isDead)
        {
            health--;
            if (health < 1)
            {
                playerAnimation.DeathAnim();
                isDead = true;
            }
        }
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 currentVelocity = new Vector2(horizontal * speed, playerRb.velocity.y);
        playerRb.velocity = currentVelocity;
        playerAnimation.MoveAnim(horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            playerAnimation.JumpAnim(true);
            StartCoroutine(ResetJump());
        }
        
                
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true) playerAnimation.AttackAnim() ;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, ground);
        

        if (hit.collider != null)
        {
            if (resetJump == false)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJump()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
        yield return new WaitForSeconds(1.3f);
        playerAnimation.JumpAnim(false);
    }

}
