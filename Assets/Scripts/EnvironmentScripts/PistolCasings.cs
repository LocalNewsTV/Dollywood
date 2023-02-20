using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolCasings : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 7);
        //StartCoroutine(DieInFiveSeconds());
    }
}
