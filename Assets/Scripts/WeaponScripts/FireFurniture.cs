using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFurniture : MonoBehaviour
{
    [SerializeField] public float speed = 30f;
    private float bulletRange = 250f;
    private Vector3 init;
    // Update is called once per frame
    private void Start()
    {
        init = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(new Vector3(0,0, speed * Time.deltaTime));
        if (Vector3.Distance(transform.position, init) > bulletRange)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
       
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Success");
            }
    }
}
