using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public GameObject Particles;
    public AudioSource Sound;
    public AudioClip HitSound;
    // Start is called before the first frame update
    private void Start()
    {
        soundFind();
    }
    private void Update()
    {
        if(Sound==null)
        {
            soundFind();
        }
    }
    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        if(HitSound!=null)
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
    void soundFind()
    {
        Sound = GetComponent<AudioSource>();
    }
}
