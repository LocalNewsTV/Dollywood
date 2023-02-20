using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed = 9f;    // Start is called before the first frame update
    float x;
    float y;
    float z;
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.z < -100) {
            transform.position = new Vector3(x, y, 100);
        } else if (transform.position.z > 100){
            transform.position = new Vector3(x, y, -100);
        }
    }
}
