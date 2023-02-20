using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicateUp : MonoBehaviour
{
    [SerializeField] BossController bc;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I have been hit");
        int damage = 0;
        if (other.CompareTag("RPG")) { damage = 50; }
        else if (other.CompareTag("bullet")) { damage = 10; }
        else if (other.CompareTag("PlayerMelee")) { damage = 8; }
        if (damage > 0) { bc.TakeDamage(damage); }

    }
}
