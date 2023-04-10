using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a particle explosion at position in Trigger
/// </summary>
public class RPGExplosion : MonoBehaviour
{
    public GameObject Explosion;
    private GameObject ExplosionReference;
    void OnTriggerEnter(Collider other){
        ExplosionReference = Instantiate(Explosion);
        ExplosionReference.transform.position = gameObject.transform.position;
    }
}
