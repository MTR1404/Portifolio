using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim, swordAnim;
    SpriteRenderer spriteRenderer, swordSpriteRenderer;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        swordAnim = transform.GetChild(1).GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        swordSpriteRenderer = transform.GetChild(1).GetComponentInChildren <SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void MoveAnim( float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));

        if(move < 0) spriteRenderer.flipX = true;
        else if (move > 0) spriteRenderer.flipX = false;
    }

    public void JumpAnim(bool canJump)
    {
        anim.SetBool("Jumping", canJump);
    }

    public void AttackAnim()
    {
        anim.SetTrigger("Attack");
        swordAnim.SetTrigger("Slash");

        if (spriteRenderer.flipX == true) swordSpriteRenderer.flipX = true;

        else swordSpriteRenderer.flipX = false;
    }

    public void DeathAnim()
    {
        anim.SetTrigger("Death");
    }
}
