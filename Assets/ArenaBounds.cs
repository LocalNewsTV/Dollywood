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
    private void OnStartBossFight()
    {
        GetComponent<MeshCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered Platform");
            CharacterController player = other.GetComponent<CharacterController>();
            player.enabled = false;
            other.transform.position = spawnPoint.position;
            player.enabled = true;
        }
    }
}
