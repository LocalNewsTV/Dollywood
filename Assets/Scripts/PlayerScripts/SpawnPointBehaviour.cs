using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Debug.Log("Spawn Point Hit");
            Messenger<Vector3>.Broadcast(GameEvent.CHANGE_SPAWN_POINT, other.transform.position);
        }
    }
}
