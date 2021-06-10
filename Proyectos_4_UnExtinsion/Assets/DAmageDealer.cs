using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAmageDealer : MonoBehaviour
{
    bool Zombie;
    private void Start()
    {
        if(transform.parent.parent.GetComponent<PalyerMovment>())
        {
            Zombie = true;
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (Zombie)
        {
            if (other.GetComponent<EnemiesHealth>())
            {
                other.GetComponent<EnemiesHealth>().TakeDamage(20);
            }
        }
        else
        {
            if(other.GetComponent<HealthZombie>())
            {
                other.GetComponent<HealthZombie>().TakeDamage(30);
            }
        }
    }
}
