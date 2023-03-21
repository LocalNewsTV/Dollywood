using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    virtual public void Open()
    {
        if (!IsActive()){
            Messenger.Broadcast(GameEvent.POPUP_OPENED);
            gameObject.SetActive(true);
        }

    }
    virtual public void Close()
    {
        if (IsActive()){
            Messenger.Broadcast(GameEvent.POPUP_CLOSED);
            gameObject.SetActive(false);
        }

    }
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
