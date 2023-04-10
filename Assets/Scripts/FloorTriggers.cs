using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for all Pickup items, uses an Enumerator to track what pickup is what, to avoid having several scripts
/// </summary>
public class FloorTriggers : MonoBehaviour
{
    [SerializeField] public triggers trig;
    [SerializeField] private bool SelfDestructAfterUse = false;
    public enum triggers
    {
        Paintings,
        Spawn1,
        Spawn2,
        Spawn3,
        Spawn4,
        UnlockElevator,
        UnlockStoreroom,
        AwakenBoss, 
        UnlockDagger,
        JustTheTip
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(trig == triggers.Paintings) { Messenger.Broadcast(GameEvent.REVEAL_PHOTOS); }
            else if(trig == triggers.Spawn1){ Messenger.Broadcast(GameEvent.ENEMY_SPAWN_A); }
            else if(trig == triggers.Spawn2){ Messenger.Broadcast(GameEvent.ENEMY_SPAWN_B); }
            else if(trig == triggers.Spawn3) { Messenger.Broadcast(GameEvent.ENEMY_SPAWN_C); }
            else if(trig == triggers.Spawn4) { Messenger.Broadcast(GameEvent.ENEMY_SPAWN_D); }
            else if(trig == triggers.UnlockStoreroom){ Messenger.Broadcast(GameEvent.UNLOCK_DOOR_A); }
            else if(trig == triggers.UnlockElevator){ Messenger.Broadcast(GameEvent.UNLOCK_EXIT); }
            else if(trig == triggers.AwakenBoss) { Messenger.Broadcast(GameEvent.START_BOSS_FIGHT); }
            else if(trig == triggers.UnlockDagger) { Messenger.Broadcast(GameEvent.DAGGER_UNLOCK); }
            if (SelfDestructAfterUse) { Destroy(this.gameObject); }
        }

        
    }
}
