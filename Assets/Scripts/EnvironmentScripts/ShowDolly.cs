using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDolly : MonoBehaviour
{
    [SerializeField] GameObject Item1;
    [SerializeField] GameObject Item2;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MeshRenderer show1 = Item1.GetComponent<MeshRenderer>();
            MeshRenderer show2 = Item2.GetComponent<MeshRenderer>();
            show1.enabled = true;
            show2.enabled = true;
            Destroy(this.gameObject);
        }
    }
}
