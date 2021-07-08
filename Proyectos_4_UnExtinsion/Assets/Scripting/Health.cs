using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public GameObject Particles;
    AudioSource Sound;
    public AudioClip HitSound;
    // Start is called before the first frame update
    private void Start()
    {
        Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        Sound.PlayOneShot(HitSound);
        Instantiate(Particles, transform.position + Vector3.up, Quaternion.identity);
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
