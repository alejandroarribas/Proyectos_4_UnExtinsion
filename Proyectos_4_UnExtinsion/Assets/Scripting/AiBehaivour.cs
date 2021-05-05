using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaivour : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator Anim;
    public GameObject Player;
    public GameObject Path;
    public float speed = 2;
    public GameObject[] Positions;
    public GameObject Object;
    public int ActualPoint;
    public float FieldOfVew;
    public bool detected;
    public float DetectDistance;
    public float Timer = 2;
    public Transform Position;
    float ActualTimer;
    // Start is called before the first frame update
    void Start()
    {
        detected = false;
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        Player = FindObjectOfType<PalyerMovment>().gameObject;
        Positions = new GameObject[Path.transform.childCount];
        for (int i = 0; i < Positions.Length; i++)
        {
            Positions[i] = Path.transform.GetChild(i).gameObject;
        }
        ActualTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        Animations();
        if (!detected)
        {
            FollowPath();
            DetectPlayer();
        }
        if(detected)
        {
            FollowPosition();
        }

    }
    public void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= DetectDistance)
        {
            Vector3 Direction = Player.transform.position - transform.position;
            float angle = Vector3.Angle(Direction, transform.forward);
            if (angle < FieldOfVew * 0.5f)
            {
                RaycastHit Out;
                if (Physics.Raycast(transform.position, Direction, out Out, DetectDistance))
                {
                    if (Out.transform.gameObject == Player)
                    {
                        detected = true;
                        DetectPlayer();
                    }
                }
            }

        }
    }
    void FollowPath()
    {
        Agent.SetDestination(Positions[ActualPoint].transform.position);
        if(Vector3.Distance(transform.position,Positions[ActualPoint].transform.position)<1)
        {
            Agent.speed = 0;
            ChangePosition();
            
        }
    }
    void ChangePosition()
    {
        ActualTimer -= Time.deltaTime;
        if (ActualTimer <= 0)
        {
            Agent.speed = speed;
            ActualPoint += 1;
            if (ActualPoint >= Positions.Length)
            {
                ActualPoint = 0;
            }
            ActualTimer = Timer;
        }
    }
    void Animations()
    {
        Anim.SetFloat("Speed", Agent.speed);
    }
    void FollowPosition()
    {
        Agent.speed = speed;
        Agent.SetDestination(Position.position);
       
        if (Vector3.Distance(transform.position, Position.position) <= 0.2f)
        {
            Agent.speed = 0;
            ActualTimer -= Time.deltaTime;
            if (ActualTimer <= 0)
            {
                Agent.speed = speed;
                detected = false;
                Destroy(Object);
                ActualTimer = Timer;
            }
        }
    }
    
}
