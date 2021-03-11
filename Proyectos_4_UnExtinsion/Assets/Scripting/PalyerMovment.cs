using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerMovment : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    float RotateSpeed;
    public float TimeRotate;
    CharacterController Char;
    public Transform CameraTransform;
    public Vector3 Move;
    Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        Char = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float Xmove = Input.GetAxis("Horizontal");
        float Zmove = Input.GetAxis("Vertical");
        Move = new Vector3(Xmove, 0, Zmove).normalized;
       
        
        if (Xmove!=0||Zmove!=0)
        {
            float AngleTo = Mathf.Atan2(Move.x, Move.z) * Mathf.Rad2Deg + CameraTransform.eulerAngles.y;
            float AngleFinal = Mathf.SmoothDampAngle(transform.eulerAngles.y, AngleTo, ref RotateSpeed, TimeRotate);
            transform.rotation = Quaternion.Euler(0, AngleFinal, 0);
            Char.Move(transform.forward * Speed * Time.deltaTime);
        }
    }
}
