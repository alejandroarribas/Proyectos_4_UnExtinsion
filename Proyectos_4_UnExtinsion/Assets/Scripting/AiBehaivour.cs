using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaivour : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator Anim;
    AudioSource Sound;
    public AudioClip HitSound;
    public Transform LookPosition;
    ZombieOrde manager;
    PalyerMovment[] Enemigos;
    public GameObject Path;
    public float speed = 2;
    GameObject[] Positions;
    public float DAmage;
    [HideInInspector]
    public GameObject Rival;
    int ActualPoint;
    public float FieldOfVew;
    [HideInInspector]
    public bool detected;
    public float DetectDistance;
    public float Timer = 2;
    public float TimerAtak;
    [HideInInspector]
    public GameObject Object;
    [HideInInspector]
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
        Sound = GetComponent<AudioSource>();
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
        if (Enemigos != null)
        {
            for (int i = 0; i < Enemigos.Length; i++)
            {
                if (Enemigos[i] != null)
                {
                    if (Vector3.Distance(LookPosition.position, Enemigos[i].transform.position) <= DetectDistance)
                    {
                        Rival = Enemigos[i].gameObject;
                        Vector3 Direction = (Rival.transform.position+Vector3.up) - LookPosition.position;
                        float angle = Vector3.Angle(Direction, LookPosition.forward);
                        if (angle < FieldOfVew * 0.5f)
                        {

                            
                            

                            Debug.LogWarning("Visualización");
                            RaycastHit Out;
                            Ray Detectpos = new Ray(LookPosition.position, Direction);
                            Debug.DrawLine(LookPosition.position, Direction * DetectDistance,Color.red);
                            if (Physics.Raycast(Detectpos, out Out, DetectDistance))
                            {
                                if (Out.transform.gameObject == Rival)
                                {
                                    detected = true;
                                    Position = Enemigos[i].transform;
                                }
                                
                            }
                            //else
                            //{

                            //    Debug.LogWarning("Objt");
                            //    detected = true;
                            //    Position = Enemigos[i].transform;
                            //    Rival = Enemigos[i].gameObject;
                            //}
                        }

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
                transform.LookAt(Rival.transform.position);
                    if(!Rival.GetComponent<PalyerMovment>().controlled)
                    {
                        Rival.GetComponent<PalyerMovment>().CallOn(transform);
                    }
            }
            if (melee)
            {
                Agent.SetDestination(Rival.transform.position);
                if (Vector3.Distance(transform.position, Rival.transform.position) < 1.5f)
                {   
                    Anim.SetBool("Attack",true);
                    if (HitSound != null)
                        Sound.PlayOneShot(HitSound);
                    timerreset -= Time.deltaTime;

                    if (timerreset <= 0)
                    {
                        
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
    public void MAkeDamage()
    {
       if(RAnge)
       {
            if (HitSound != null)
                Sound.PlayOneShot(HitSound);
       }
        Rival.GetComponent<HealthZombie>().TakeDamage(DAmage);
    }
    
}
