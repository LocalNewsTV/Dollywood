using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBounds : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    private void Awake(){
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }
    /// <summary>
    /// Sets barrier of arena to become active so player doesnt' fall off the arena
    /// </summary>
    private void OnStartBossFight(){
        GetComponent<MeshCollider>().enabled = true;
    }
    /// <summary>
    /// If Player hits the triggers, forces player to respawn
    /// </summary>
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            other.GetComponent<PlayerCharacter>().Respawn();
        }
    }
}
