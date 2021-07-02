using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaivour : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator Anim;
    public Transform LookPosition;
    public ZombieOrde manager;
    public PalyerMovment[] Enemigos;
    public GameObject Path;
    public float speed = 2;
    public GameObject[] Positions;
   
    public GameObject Rival;
    public int ActualPoint;
    public float FieldOfVew;
    public bool detected;
    public float DetectDistance;
    public float Timer = 2;
    public float TimerAtak;
    public GameObject Object;
    public Transform Position;
    float ActualTimer;
    public float timerreset;
    public bool Scape;
    public bool melee;
    public bool RAnge;
    PlayerStateManager PSM;
    // Start is called before the first frame update
    void Start()
    {
        detected = false;
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        manager = FindObjectOfType<ZombieOrde>();
       
        Positions = new GameObject[Path.transform.childCount];
        for (int i = 0; i < Positions.Length; i++)
       {
           Positions[i] = Path.transform.GetChild(i).gameObject;
       }
       ActualTimer = Timer;
        TimerAtak = timerreset;
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemigos = manager.zombiesInScene;
        Animations();
        if (!detected)
        {
            Anim.SetBool("Attack", false); 
            FollowPath();
            DetectPlayer();
        }
        if(detected)
        {

            AttackObjetive();
        }
        

    }
    public void DetectPlayer()
    {
       
        for (int i = 0; i < Enemigos.Length; i++)
        {
            if (Vector3.Distance(LookPosition.position, Enemigos[i].transform.position) <= DetectDistance)
            {
                
                Vector3 Direction = Enemigos[i].transform.position - LookPosition.position;
                float angle = Vector3.Angle(Direction, LookPosition.forward);
                if (angle < FieldOfVew * 0.5f)
                {
                    
                    RaycastHit Out;
                    Ray Detectpos = new Ray(LookPosition.position, Direction);
                    if (Physics.Raycast(Detectpos, out Out, DetectDistance*2f))
                    {
                       
                    }
                    else
                    {
                        detected = true;
                        Position = Enemigos[i].transform;
                        Rival = Enemigos[i].gameObject;
                        
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
   
    
    public void AttackObjetive()
    {
        if (Rival == null)
        {
            detected = false;
        }
        else
        {
            if (RAnge)
            {
                Anim.SetBool("Attack",true);

                timerreset -= Time.deltaTime;

                if (timerreset <= 0)
                {
                    Rival.GetComponent<HealthZombie>().TakeDamage(12);
                    if(!Rival.GetComponent<PalyerMovment>().controlled)
                    {
                        Rival.GetComponent<PalyerMovment>().CallOn(transform);
                    }
                    timerreset = TimerAtak;
                }
            }
            if (melee)
            {
                Agent.SetDestination(Rival.transform.position);
                if (Vector3.Distance(transform.position, Rival.transform.position) < 1.5f)
                {   
                    Anim.SetBool("Attack",true);
                    timerreset -= Time.deltaTime;

                    if (timerreset <= 0)
                    {
                        Rival.GetComponent<HealthZombie>().TakeDamage(15);
                        if (!Rival.GetComponent<PalyerMovment>().controlled)
                        {
                            Rival.GetComponent<PalyerMovment>().CallOn(transform);
                        }
                        timerreset = TimerAtak;
                    }
                }
                else
                {
                    Anim.SetBool("Attack", false);
                }
            }
            if (Scape)
            {
                Anim.SetBool("Attack",true);
            }

        }
    }
    
}
