using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipTrigger : MonoBehaviour
{
    [SerializeField] string tip = "";
    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Messenger<string>.Broadcast(GameEvent.TIP_RECEIVED, tip);
        }
    }
}
