using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Dead", 2);
    }

    // Update is called once per frame
   void Dead()
    {
        Destroy(gameObject);
    }
}
