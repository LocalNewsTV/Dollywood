using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : BasePopup
{
    [SerializeField] private SettingsPopup settingsPopup;
    override public void Open() {
        base.Open();
    }
    override public  void Close()
    {
        base.Close();
    }
    public void OnSettingsButton() {
        Messenger.Broadcast(GameEvent.POPUP_OPENED);
        settingsPopup.gameObject.SetActive(true);
        Close();
    }
    public void OnExitGameButton() {
        Messenger.Broadcast(GameEvent.RETURN_TO_MAIN_MENU);
    }
    public void OnReturnToGameButton() {
        Close();
    }

    public void OnRestartButton()
    {
        Messenger.Broadcast(GameEvent.RESTART_CURRENT_MAP);
    }
}
