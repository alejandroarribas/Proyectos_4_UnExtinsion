using System;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController Char;
    Animator Anim;
    GameObject MeshPLayer;
    public Transform CamFeet;
    public Camera cam;
    public float speed;
    public float Smooth;
    float turn;
    Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Char = GetComponent<CharacterController>();
        Anim = GetComponentInChildren<Animator>();
        MeshPLayer = transform.GetChild(0).gameObject;
        CamFeet = transform.GetChild(1);
        cam = GetComponentInChildren<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
       AnimationControll();
        Gravity();
        CameraRotation();
    }
    void AnimationControll()
    {
        Anim.SetFloat("Speed", Direction.magnitude);
    }
    void Movement()
    {
        float Xmove = Input.GetAxis("Horizontal");
        float Zmove = Input.GetAxis("Vertical");
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
        float verticalRotation = -MouseY;
        
        CamFeet.Rotate(verticalRotation, 0, 0);
    }
}
