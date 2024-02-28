using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }
    [SerializeField] GameObject diamond;
    public override void Start() 
    {
        base.Start();
        Health = base.health;
    }
    public void Damage()
    {
        Health--;
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            GameObject _diamond = Instantiate(diamond, transform.position, Quaternion.identity);
            Diamond Diamond = diamond.GetComponent<Diamond>();
            Diamond.gems = base.gems;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
    }
}