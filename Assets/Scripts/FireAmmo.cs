using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    [SerializeField] public float speed = 30f;
    private float bulletRange = 150f;
    private Vector3 init;
    // Update is called once per frame
    private void Start()
    {
        init = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, init) > bulletRange)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Success");
        }
        Destroy(this.gameObject);
    }
}
