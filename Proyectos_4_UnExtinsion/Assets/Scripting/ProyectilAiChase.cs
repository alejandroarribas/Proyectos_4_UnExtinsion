using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilAiChase : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sc;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        sc.isTrigger = true;
        sc.radius = 3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AiBehaivour>())
        {
            other.GetComponent<AiBehaivour>().detected = true;
            other.GetComponent<AiBehaivour>().Position = this.transform;
            other.GetComponent<AiBehaivour>().Object = this.gameObject;
        }
    }
}
