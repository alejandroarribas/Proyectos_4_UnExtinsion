using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZombie : MonoBehaviour
{
    public GameObject AttackPoint;
    // Start is called before the first frame update
    private void Start()
    {
        EndAttack();
    }
    public void StartAttack()
    {
        AttackPoint.SetActive(true);
    }
    public void EndAttack()
    {
        AttackPoint.SetActive(false); 
    }
}
