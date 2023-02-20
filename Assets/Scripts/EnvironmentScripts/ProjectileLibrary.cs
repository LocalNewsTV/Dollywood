using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLibrary : MonoBehaviour
{

    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    [SerializeField] private GameObject three;
    [SerializeField] private GameObject four;
    [SerializeField] private GameObject five;
    private GameObject[] collection;
    private void Start()
    {
        collection = new GameObject[] { one, two, three, four, five};
    }
    public GameObject SelectItem(){
        return collection[Random.Range(0, collection.Length)];
    }
}
