using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Dead", time);
    }

    // Update is called once per frame
   void Dead()
    {
        Destroy(gameObject);
    }
}
