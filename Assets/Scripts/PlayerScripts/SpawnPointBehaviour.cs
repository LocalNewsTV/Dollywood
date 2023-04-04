using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    private void SetSpawn()
    {
        Debug.Log("Spawn Set");
        Messenger<Vector3>.Broadcast(GameEvent.CHANGE_SPAWN_POINT, gameObject.transform.position);
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            SetSpawn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetSpawn();
        }
    }
}
