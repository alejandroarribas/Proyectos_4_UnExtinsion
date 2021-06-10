using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealth : Health
{
    public GameObject Zombie;
    ZombieOrde orde;
    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        orde = FindObjectOfType<ZombieOrde>();
    }

    // Update is called once per frame
    public override void Die()
    {
        Instantiate(Zombie, transform.position, Quaternion.identity);
        orde.NewZombie();
        Destroy(gameObject);
        base.Die();
    }
}
