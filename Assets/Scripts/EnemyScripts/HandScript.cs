using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
