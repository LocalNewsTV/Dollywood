using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject casing;
    [SerializeField] private Transform spawnPos;
    private GameObject casingref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            casingref = Instantiate(casing) as GameObject;
            casingref.transform.position = spawnPos.TransformPoint(0, 0, 0);
            casingref.transform.rotation = transform.rotation;
            casingref.GetComponent<Rigidbody>().AddForce(10, 5, 0);
        }
    }
}
