using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerCharacter pc;
    [SerializeField] string thisMap;

    public void Awake()
    {
        Messenger.AddListener(GameEvent.DAGGER_UNLOCK, OnDaggerUnlock);
        Messenger.AddListener(GameEvent.PISTOL_UNLOCK, OnPistolUnlock);
        Messenger.AddListener(GameEvent.PISTOL_AMMO_PICKUP, OnPistolAmmoPickup);
        Messenger.AddListener(GameEvent.SWORD_UNLOCK, OnSwordUnlock);
        Messenger.AddListener(GameEvent.RPG_UNLOCK, OnRPGUnlock);
        Messenger.AddListener(GameEvent.RPG_AMMO_PICKUP, OnRPGAmmoPickup);
        Messenger.AddListener(GameEvent.HEALTH_KIT_PICKUP, OnHealthKitPickup);
        Messenger.AddListener(GameEvent.RETURN_TO_MAIN_MENU, OnMoveToMainMenu);
        Messenger.AddListener(GameEvent.RESTART_CURRENT_MAP, OnRestartCurrentMap);
        Messenger.AddListener(GameEvent.NEXT_LEVEL, OnNextLevel);
    }
    public void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.DAGGER_UNLOCK, OnDaggerUnlock);
        Messenger.RemoveListener(GameEvent.PISTOL_UNLOCK, OnPistolUnlock);
        Messenger.RemoveListener(GameEvent.PISTOL_AMMO_PICKUP, OnPistolAmmoPickup);
        Messenger.RemoveListener(GameEvent.SWORD_UNLOCK, OnSwordUnlock);
        Messenger.RemoveListener(GameEvent.RPG_UNLOCK, OnRPGUnlock);
        Messenger.RemoveListener(GameEvent.RPG_AMMO_PICKUP, OnRPGAmmoPickup);
        Messenger.RemoveListener(GameEvent.HEALTH_KIT_PICKUP, OnHealthKitPickup);
        Messenger.RemoveListener(GameEvent.RETURN_TO_MAIN_MENU, OnMoveToMainMenu);
        Messenger.RemoveListener(GameEvent.RESTART_CURRENT_MAP, OnRestartCurrentMap);
        Messenger.RemoveListener(GameEvent.NEXT_LEVEL, OnNextLevel);
    }
    private void OnNextLevel(){
        pc.SaveInfo();
    }
    private void OnRestartCurrentMap()
    {
        SceneManager.LoadScene(thisMap);
    }
    private void OnMoveToMainMenu(){ SceneManager.LoadScene(GameTerms.MAIN_MENU);}
    private void OnDaggerUnlock(){ pc.OnDaggerUnlock(); }
    private void OnPistolUnlock(){ pc.OnPistolUnlock(); }
    private void OnSwordUnlock(){ pc.OnSwordUnlock(); }
    private void OnRPGUnlock() { pc.OnRPGUnlock(); }
    private void OnRPGAmmoPickup() { pc.OnRPGAmmoPickup(); }
    private void OnHealthKitPickup() { pc.OnHealthKitPickup(); }
    private void OnPistolAmmoPickup() { pc.OnPistolAmmoPickup(); }

}
