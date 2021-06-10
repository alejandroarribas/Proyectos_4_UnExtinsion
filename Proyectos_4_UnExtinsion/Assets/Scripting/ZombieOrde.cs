using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOrde : MonoBehaviour
{
    public PalyerMovment[] zombiesInScene;
    public PalyerMovment[] zombiesAi;
    public PalyerMovment Player;
    // Start is called before the first frame update
    void Start()
    {
        NewZombie();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.gameObject.GetComponent<HealthZombie>().TakeDamage(20);
          
          
        }
    }
    // Update is called once per frame
    public void ChangePlayer()
    {
        
        int Randomzombie = Random.RandomRange(0, zombiesAi.Length);
        Destroy(Player.gameObject);
        zombiesAi[Randomzombie].ChangeControlled();
        Delay();
        
    }
    public void Delay()
    {
        Invoke("NewZombie", 0.25f);
    }
    public void NewZombie()
    {
        zombiesAi = null;
        zombiesInScene = null;
        zombiesInScene = FindObjectsOfType<PalyerMovment>();
        zombiesAi = new PalyerMovment[zombiesInScene.Length - 1];
        Player = GameObject.FindWithTag("Player").GetComponent<PalyerMovment>();
        for (int i = 0; i < zombiesInScene.Length; i++)
        {
            if (!zombiesInScene[i].controlled)
            {
                for (int j = 0; j < zombiesAi.Length; j++)
                {
                    if (zombiesAi[j] == null)
                    {
                        zombiesAi[j] = zombiesInScene[i];
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < zombiesAi.Length; i++)
        {
            if (!zombiesAi[i].controlled)
            {
                zombiesAi[i].PlayerLocation(Player.transform);
            }
        }
    }
 
}
