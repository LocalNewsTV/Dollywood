using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireObjects : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject defaultObj;

    [SerializeField] string partName;
    private GameObject ammunition;
    private Vector3 big = new Vector3(4, 4, 4);

}
