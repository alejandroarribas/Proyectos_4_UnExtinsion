using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PalyerMovment : MonoBehaviour
{
    CharacterController Char;
    NavMeshAgent Agent;
    Animator Anim;
    GameObject MeshPLayer;
    Transform CamFeet;
    public Transform PLayerPos;

    public Transform Objetivo;
    public Camera cam;
    public float speed;
    public float Smooth;
    public bool controlled;
    public bool Combat;
    float turn;
    float Realspeed;
    float Xmove;
    float Zmove;

    Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Char = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
        MeshPLayer = transform.GetChild(0).gameObject;
        CamFeet = transform.GetChild(1);
        cam = GetComponentInChildren<Camera>();
        Realspeed = speed;
       
        if(!controlled)
        {
            CamFeet.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controlled)
        {
            Movement();
            AnimationControll();
            Gravity();
            CameraRotation();
            ActiveInput();
        }
        else
        {
            AnimationControll();
            IAControlled();
        }
    }
    void AnimationControll()
    {
        if (controlled)
        {
            Anim.SetFloat("Speed", Direction.magnitude);
        }
        else
        {
            Anim.SetFloat("Speed", Agent.speed);
        }
    }

    //PlayerControl
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Movement()
    {
        Xmove = Input.GetAxis("Horizontal");
        Zmove = Input.GetAxis("Vertical");
        Direction = new Vector3(Xmove, 0, Zmove).normalized;
        if (Direction.magnitude >= 0.1f)
        {
            float Angle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float finalAngle = Mathf.SmoothDampAngle(MeshPLayer.transform.eulerAngles.y, Angle, ref turn, Smooth);
            MeshPLayer.transform.rotation = Quaternion.Euler(MeshPLayer.transform.up * finalAngle);
            Char.Move(MeshPLayer.transform.forward * speed * Time.deltaTime);
        }

    }
    void Gravity()
    {
        Char.Move(transform.up * -speed * Time.deltaTime);
    }
    void CameraRotation()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, MouseX, 0);
        if(Xmove==0 && Zmove==0)
        {
            MeshPLayer.transform.Rotate(0, -MouseX, 0);
        }
        float verticalRotation = -MouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);
        CamFeet.Rotate(verticalRotation, 0, 0);

    }
    public void ChangeControlled()
    {
        controlled = true;
        transform.tag = "Player";
        Agent.enabled = false;
        CamFeet.gameObject.SetActive(true);
    }
    void ActiveInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Anim.SetTrigger("Attack");
            
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Anim.SetTrigger("Scream");
        }
    }

    //AiControllPlayer
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void PlayerLocation(Transform PlayerT)
    {
        PLayerPos = PlayerT;
    }
    void IAControlled()
    {
        if (!Combat)
            FollowPlayer();
        else
            FollowEnemie();
    }
    void FollowPlayer()
    {
        if (PLayerPos != null)
        {
            Agent.SetDestination(PLayerPos.position);


            if (Vector3.Distance(transform.position, PLayerPos.position) < 1.5f)
            {
                Agent.speed = 0;
            }
            else
            {
                Agent.speed = speed;
            }
        }
        else
        {
            return;
        }
    }
    public void FollowEnemie()
    {
        if(Objetivo==null)
        {
            Combat = false;
        }
        
        Agent.SetDestination(Objetivo.position);
        if(Vector3.Distance(transform.position,Objetivo.position)<1.5)
        {
            Anim.SetTrigger("Attack");
           
        }
        if (Vector3.Distance(transform.position, Objetivo.position) > 10)
        {
            Combat = false;
        }
    }
    
   
    
    public void CallOn(Transform position)
    {
       
        Objetivo = position;
        Combat = true;
    }

}
