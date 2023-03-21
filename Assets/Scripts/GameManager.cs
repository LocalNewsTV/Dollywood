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
        Messenger<string>.AddListener(GameEvent.RESTART_CURRENT_MAP, OnRestartCurrentMap);
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
        Messenger.AddListener(GameEvent.RETURN_TO_MAIN_MENU, OnMoveToMainMenu);
        Messenger<string>.AddListener(GameEvent.RESTART_CURRENT_MAP, OnRestartCurrentMap);
    }
    private void OnRestartCurrentMap(string map)
    {
        SceneManager.LoadScene(map);
    }
    private void OnMoveToMainMenu()
    {
        SceneManager.LoadScene(GameTerms.MAIN_MENU);
    }
    private void OnDaggerUnlock()
    {
        PlayerPrefs.SetInt(GameTerms.DAGGER_UNLOCKED, 1);
    }
    private void OnPistolUnlock()
    {
        pc.OnPistolUnlock();
        PlayerPrefs.SetInt(GameTerms.PISTOL_UNLOCKED, 1);
    }
    private void OnSwordUnlock()
    {
        pc.OnSwordUnlock();
        PlayerPrefs.SetInt(GameTerms.SWORD_UNLOCKED, 1);
    }
    private void OnRPGUnlock()
    {
        pc.OnRPGUnlock();
        PlayerPrefs.SetInt(GameTerms.RPG_UNLOCKED, 1);

    }
    private void OnRPGAmmoPickup() { pc.OnRPGAmmoPickup(); }
    private void OnHealthKitPickup() { pc.OnHealthKitPickup(); }
    private void OnPistolAmmoPickup() { pc.OnPistolAmmoPickup(); }

}
