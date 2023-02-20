using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public float speed = 10;
    [SerializeField] private Rotations axis = Rotations.RotateZ;
    public enum Rotations{
        RotateY,
        RotateX, 
        RotateZ,
    }
    // Update is called once per frame
    void Update()
    {
        if (axis == Rotations.RotateZ) {
            transform.Rotate(new Vector3(0, 0, 8) * speed * Time.deltaTime, Space.World);
        }
        else if(axis == Rotations.RotateY){
            transform.Rotate(new Vector3(0, 8, 0) * speed * Time.deltaTime, Space.World);
        }
        else if(axis == Rotations.RotateX){
            transform.Rotate(new Vector3(8, 0, 0) * speed * Time.deltaTime, Space.World);
        }
    }
}
