using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrio : MonoBehaviour
{
    [SerializeField] GameObject pickupItem;
    private bool itemSpawning = false;
    private float timeForItemRespawn = 10f;

    private void Start()
    {
        Instantiate(pickupItem, this.transform);
    }
    private IEnumerator RespawnObject()
    {
        yield return new WaitForSeconds(timeForItemRespawn);
        Instantiate(pickupItem, this.transform);
        itemSpawning = false;
    }
    void Update()
    {
       if(!itemSpawning && transform.childCount == 0){
            itemSpawning = true;
            StartCoroutine(RespawnObject());
        }
    }
}
