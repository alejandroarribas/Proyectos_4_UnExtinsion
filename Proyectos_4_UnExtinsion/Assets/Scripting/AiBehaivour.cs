using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaivour : MonoBehaviour
{
    NavMeshAgent Agent;
    public GameObject Player;
    public float FieldOfVew;
    public bool detected;
    public float DetectDistance;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,Player.transform.position)<=DetectDistance)
        {
            Vector3 Direction = Player.transform.position - transform.position;
            float angle = Vector3.Angle(Direction, transform.forward);
            if(angle<FieldOfVew*0.5f)
            {
                RaycastHit Out;
                if(Physics.Raycast(transform.position,Direction,out Out,DetectDistance))
                {
                    if(Out.transform.gameObject == Player)
                    {
                        detected = true;
                        Agent.SetDestination(Player.transform.position);
                    }
                }
            }

        }
    }
    public void DetectPlayer()
    {

    }
    
}
