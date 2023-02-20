using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    public float minVert = -40f;
    public float maxVert = 38f;
    private float rotationX = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            rotationX += speed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        }
        else{
            rotationX -= speed;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        }
        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}
