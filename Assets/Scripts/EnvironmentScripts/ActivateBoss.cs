using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField] private BossController bc;
    [SerializeField] private SoundController sc;
    [SerializeField] private GameObject Bounds;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Bounds.SetActive(true);
            //sc.DarkColossus();
            bc.Awaken();
            Destroy(this.gameObject);
        }
    }
}
