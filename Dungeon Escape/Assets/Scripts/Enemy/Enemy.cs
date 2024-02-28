using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 targetPos;
    protected bool isHit = false;
    protected Player player;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    public virtual void Start()
    {
        targetPos = pointB.position;
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent <Player> (); ;
    }
    public virtual void Attack() { }
    public virtual void Update()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false) || anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        spriteRenderer.flipX = Flip();
        Movement();
    }

    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            targetPos = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            targetPos = pointA.position;
            anim.SetTrigger("Idle");
        }

        if(!isHit) transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 dir = player.transform.position - transform.position;
        if (anim.GetBool("InCombat") == true)
        {
            if (dir.x < 0) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }
    }

    public bool Flip()
    {
        if (targetPos == pointA.position) return true;
        return false;
    }
}
