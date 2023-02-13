using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float x;
    private float z;
    private float y;
    public float rotationSpeed = 45f;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.rotation.x;
        z = transform.rotation.z;
        y = transform.rotation.y;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("GameObject Destroyed");
        Destroy(this.gameObject);
    }
    void Update()
    {
        float rotateY = rotationSpeed * Time.deltaTime % 360;
        transform.Rotate(new Vector3(0, rotateY, 0 ), Space.World);
    }
}
