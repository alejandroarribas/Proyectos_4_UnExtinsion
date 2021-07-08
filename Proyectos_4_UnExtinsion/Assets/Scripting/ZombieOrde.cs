using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZombieOrde : MonoBehaviour
{
    public PalyerMovment[] zombiesInScene;
    public PalyerMovment[] zombiesAi;
    public Text ZombieNumber;
    public Image PlayerHealth;
    public PalyerMovment Player;
    // Start is called before the first frame update
    void Start()
    {
        NewZombie();
    }

    private void Update()
    {
        PlayerHealth.rectTransform.localScale = new Vector3(Player.GetComponent<HealthZombie>().health / Player.GetComponent<HealthZombie>().MaxHealth,
            PlayerHealth.rectTransform.localScale.y, PlayerHealth.rectTransform.localScale.z);
        ZombieNumber.text = "Numero de zombies: "+zombiesInScene.Length.ToString();
    }
    // Update is called once per frame
    public void ChangePlayer()
    {
        
        int Randomzombie = Random.RandomRange(0, zombiesAi.Length);
        Destroy(Player.gameObject);
        if(zombiesAi.Length>=0)
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
    public void Regrup()
    {
        for (int i = 0; i < zombiesAi.Length; i++)
        {
            if(zombiesAi[i].Combat)
            {
                zombiesAi[i].Combat = false;
                
            }
        }
    }
    public void Rampage(Transform Enemigo)
    {
        for (int i = 0; i < zombiesAi.Length; i++)
        {
            if (!zombiesAi[i].Combat)
            {
                zombiesAi[i].Combat = true;
                zombiesAi[i].Objetivo = Enemigo;

            }
        }
    }
 
}
