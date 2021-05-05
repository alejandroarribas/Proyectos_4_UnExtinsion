using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerMovment : MonoBehaviour
{
    [Header("Movement Var")]
    public float Speed;
    public float JumpForce;
    float RotateSpeed;
    public float TimeRotate;
    public float Gravity;
   
    [Header("TestVar")]
    public Transform CameraTransform;
    public Vector3 Move;
    public bool Grounded;
    [Header("Detectors")]
    public Vector3 FloorDetector;
    public Vector3 newFloorDetection;
    public float floorDistance;

    Rigidbody Rb;
    public Vector3 movimiento;
    public Vector3 movimientoHorizontal;
    public GameObject Priyectil;
    public GameObject Instancia;
    public Transform Spawn;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        newFloorDetection = transform.forward;
        FloorDetector = -transform.up;
        float Xmove = Input.GetAxis("Horizontal");
        float Zmove = Input.GetAxis("Vertical");
        float MouseX = Input.GetAxis("Mouse X");
        Move = new Vector3(Xmove, 0, Zmove).normalized;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instancia = Instantiate(Priyectil, Spawn.position, Quaternion.identity);
            Instancia.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 300);
        }
        transform.Rotate(0, MouseX, 0);
        if (Xmove!=0||Zmove!=0)
        {
            //float AngleTo = Mathf.Atan2(Move.x, Move.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
            //float AngleFinal = Mathf.SmoothDampAngle(transform.eulerAngles.y, AngleTo, ref RotateSpeed, TimeRotate);
            //transform.rotation = Quaternion.Euler(0, AngleFinal, 0);
            movimientoHorizontal = transform.right * Xmove+transform.forward*Zmove; 
        }
        else
        {
            movimientoHorizontal = Vector3.zero;
        }
        if (!Grounded)
        {
            movimiento.y -= Gravity * Time.deltaTime;
        }
        else
        {
            movimiento.y = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&Grounded)
        {
            movimiento.y = JumpForce*Time.deltaTime;
        }
        Rb.velocity=(movimiento+movimientoHorizontal);
    }
    private void FixedUpdate()
    {
        Ray ForwardDetection = new Ray(transform.position+transform.up*0.5f, newFloorDetection);
        Ray FloorDetection = new Ray(transform.position, FloorDetector);
        RaycastHit FloorHit;
        RaycastHit NewFloorHit;
        if(Physics.Raycast(FloorDetection,out FloorHit,floorDistance))
        {
            Debug.Log("Suelo");
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
        if(Physics.Raycast(ForwardDetection, out NewFloorHit, floorDistance))
        {
            transform.up = NewFloorHit.normal;
            transform.position = Vector3.MoveTowards(transform.position,NewFloorHit.point,0.2f);
        }
    }
}
