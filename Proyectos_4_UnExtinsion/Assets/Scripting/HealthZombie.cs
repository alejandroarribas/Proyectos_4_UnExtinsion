using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie : Health
{
    public PalyerMovment Self;
    ZombieOrde manager;
    // Start is called before the first frame update
    void Start()
    {
        Self = GetComponent<PalyerMovment>();
        manager = FindObjectOfType<ZombieOrde>();
        health = MaxHealth;
        
    }
    public override void TakeDamage(float Damage, Transform position)
    {
        base.TakeDamage(Damage,position);
        manager.Rampage(position);
    }

    // Update is called once per frame
    public override void Die()
    {
        if(Self.controlled)
        {
            manager.ChangePlayer();
           
        }
        else
        {
            manager.Delay();
            Destroy(Self.gameObject);
        }
        base.Die();
    }
}
