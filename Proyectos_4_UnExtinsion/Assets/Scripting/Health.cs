using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    // Start is called before the first frame update


    // Update is called once per frame
   public void TakeDamage(float Damage)
    {
        health -= Damage;
        if(health<=0)
        {
            Die();
        }
    }
    public virtual void Die()
    {

    }
}
