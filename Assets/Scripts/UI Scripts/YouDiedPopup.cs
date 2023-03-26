using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YouDiedPopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI respawnCount;
    public override void Open()
    {
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }

    public void OnExitGameButton()
    {
        Messenger.Broadcast(GameEvent.RETURN_TO_MAIN_MENU);
    }
    
    public void OnRespawnButton()
    {
        Close();
        Messenger.Broadcast(GameEvent.PLAYER_RESPAWN);
    }

    public void PlayerDied()
    {
        Debug.Log("Player Died Called");
        StartCoroutine(OnPlayerDied());
    }

    private IEnumerator OnPlayerDied()
    {
        for(int i = 2; i > 0; i--)
        {
            Debug.Log(i);
            respawnCount.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        Messenger.Broadcast(GameEvent.PLAYER_RESPAWN);
        Close();
    }
}
