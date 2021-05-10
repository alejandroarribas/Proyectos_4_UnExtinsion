using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewIkSystem : MonoBehaviour
{

    public int ChainLength;
    public Transform Target;
    public Transform Pole;
    public int IterationsPerFrame;
    public float Delta;
    [Range(0,1)]
    public float SnapBackStrength;
    public Transform[] Bones;
    public float CompleteLength;
    public Vector3[] BonesPositions;
    public float[] BonesLength;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ResolveIK();
    }
    void Init()
    {
        Bones = new Transform[ChainLength + 1];
        BonesPositions = new Vector3[ChainLength + 1];
        BonesLength = new float[ChainLength];
        CompleteLength = 0;
        var current = transform;
        for (int i = Bones.Length; i >= 0; i--)
        {
            Bones[i] = current;
            current = current.parent;
            if(i == Bones.Length - 1)
            {

            }
            else
            {
                BonesLength[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLength += BonesLength[i];
            }
        }
    }

    void ResolveIK()
    {
       if( Target ==null)
        {
            return;
        }
       if(Bones.Length!=ChainLength)
        {
            Init();
        }
    }
    private void OnDrawGizmos()
    {
        var current = this.transform;
        for (int i = 0; i < ChainLength && current != null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, 2));
            Handles.color = Color.red;
            Handles.DrawWireCube(transform.up*0.5f, Vector3.one);
            current = current.parent;
        }
    }
}
