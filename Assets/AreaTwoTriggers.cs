using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwoTriggers : MonoBehaviour
{
    [SerializeField] public triggers trig;
    [SerializeField] private bool SelfDestructAfterUse = false;
    [SerializeField] private SceneControllerLevelTwo sc;
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
            if(trig == triggers.paintings) { sc.ShowDolly(); }
            else if(trig == triggers.spawn1){ sc.SpawnOne(); }
            else if(trig == triggers.spawn2){ sc.SpawnTwo(); }
            else if(trig == triggers.spawn3) { sc.SpawnThree(); }
            else if(trig == triggers.unlockStoreroom){ sc.unlockStoreroom(); }
            else if(trig == triggers.unlockElevator){ sc.unlockElevator(); }
            if (SelfDestructAfterUse) { Destroy(this.gameObject); }
        }
        
    }
}
