using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Logic for the "You Died" Popup allowing player to Respawn, Exit or Continue
/// </summary>
public class YouDiedPopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI respawnCount;
    public override void Open(){ base.Open(); }
    public override void Close(){ base.Close(); }

    public void OnExitGameButton(){ Messenger.Broadcast(GameEvent.RETURN_TO_MAIN_MENU); }
    
    public void OnRespawnButton(){
        Close();
        Messenger.Broadcast(GameEvent.PLAYER_RESPAWN);
    }
}
