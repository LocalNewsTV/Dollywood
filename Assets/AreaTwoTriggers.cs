using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwoTriggers : MonoBehaviour
{
    [SerializeField] public triggers trig;
    [SerializeField] private bool SelfDestructAfterUse = false;
    public enum triggers
    {
        paintings,
        spawn1,
        spawn2,
        spawn3,
        unlockElevator,
        unlockStoreroom,
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(trig == triggers.paintings) { Messenger.Broadcast(GameEvent.REVEAL_PHOTOS); }
            else if(trig == triggers.spawn1){ Messenger.Broadcast(GameEvent.ENEMY_SPAWN_A); }
            else if(trig == triggers.spawn2){ Messenger.Broadcast(GameEvent.ENEMY_SPAWN_B); }
            else if(trig == triggers.spawn3) { Messenger.Broadcast(GameEvent.ENEMY_SPAWN_C); }
            else if(trig == triggers.unlockStoreroom){ Messenger.Broadcast(GameEvent.UNLOCK_DOOR_A); }
            else if(trig == triggers.unlockElevator){ Messenger.Broadcast(GameEvent.UNLOCK_EXIT); }
            if (SelfDestructAfterUse) { Destroy(this.gameObject); }
        }
        
    }
}
