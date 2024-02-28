using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }
    [SerializeField] GameObject acid, diamond;
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
    }

    public override void Attack()
    {
        Instantiate(acid,transform.position,Quaternion.identity);   
    }

    public override void Update() { /*Eat 5 Star*/ }
    public override void Movement() { /*Eat 5 Star*/ }
}
