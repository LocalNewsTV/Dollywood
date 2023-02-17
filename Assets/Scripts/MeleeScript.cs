using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    public float minVert = -40f;
    public float maxVert = 38f;
    private float rotationZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotationZ += speed * Time.deltaTime;
            rotationZ = Mathf.Clamp(rotationZ, minVert, maxVert);
        }
        else
        {
            rotationZ -= speed;
            rotationZ = Mathf.Clamp(rotationZ, minVert, maxVert);
        }
        transform.localEulerAngles = new Vector3(rotationZ, 0, 0);
    }
}
