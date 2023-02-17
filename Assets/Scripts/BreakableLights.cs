using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLights : MonoBehaviour
{
    [SerializeField] GameObject spotlight;
    [SerializeField] Material blackOut;
    [SerializeField] ParticleSystem particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            Destroy(spotlight);
            this.gameObject.GetComponent<MeshRenderer>().material = blackOut;
            particles.Play();
        }
    }
}
