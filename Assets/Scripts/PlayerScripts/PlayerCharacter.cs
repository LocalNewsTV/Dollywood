using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rpg;
    [SerializeField] private GameObject dagger;
    [SerializeField] private GameObject sword;
    //Weapon Unlocks
    private bool haveSword;
    private bool havePistol;
    private bool haveRpg;
    private bool haveDagger;

    //Ammunition + HP Variables
    private int health;
    private int pistolAmmo;
    private int rpgAmmo;

    private int maxHealth = 100;


    private GameObject active;//Currently Active Weapon ref.

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.AddListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger<int>.AddListener(GameEvent.PLAYER_TAKE_DAMAGE, OnPlayerHit);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.RemoveListener(GameEvent.GAME_ACTIVE, OnGameActive);
    }


    void Start()
    {
        haveSword = PlayerPrefs.GetInt(GameTerms.SWORD_UNLOCKED, 0) == 1;
        havePistol = PlayerPrefs.GetInt(GameTerms.PISTOL_UNLOCKED, 0) == 1;
        haveRpg = PlayerPrefs.GetInt(GameTerms.RPG_UNLOCKED, 0) == 1;
        haveDagger = PlayerPrefs.GetInt(GameTerms.DAGGER_UNLOCKED, 0) == 1;

        health = PlayerPrefs.GetInt(GameTerms.HEALTH, 100);
        Messenger<float>.Broadcast(GameEvent.PLAYER_HIT, (float)health / maxHealth);

        pistolAmmo = PlayerPrefs.GetInt(GameTerms.PISTOL_AMMO, 20);
        rpgAmmo = PlayerPrefs.GetInt(GameTerms.RPG_AMMO, 10);

        active = null;
    }
    public void Hit()
    {
        health -= 5;
        Messenger<float>.Broadcast(GameEvent.PLAYER_HIT, (float)health / 100.0f);
        if (health == 0)
        {
            Debug.Break();
        }
    }

    private void OnGameActive()
    {
        this.enabled = true;
    }
    private void OnGameInactive()
    {
        this.enabled = false;
    }
    void Update()
    {
        //Player Changed Equipment Options
        if (Input.GetButtonDown("RPG") && haveRpg){
            if (active) { active.SetActive(false); }
            active = rpg;
            active.SetActive(true);
            Messenger<int>.Broadcast(GameEvent.RPG_EQUIPPED, (int)rpgAmmo);
        }
        else if (Input.GetButtonDown("Pistol") && havePistol){
            if (active) { active.SetActive(false); }
            active = pistol;
            active.SetActive(true);
            Messenger<int>.Broadcast(GameEvent.PISTOL_EQUIPPED, (int)pistolAmmo);
        }
        else if (Input.GetButtonDown("Sword") && haveSword){
            if (active) { active.SetActive(false); }
            active = sword;
            active.SetActive(true);
            Messenger.Broadcast(GameEvent.MELEE_EQUIPPED);
        }
        else if (Input.GetButtonDown("Dagger") && haveDagger){
            if (active) { active.SetActive(false); }
            active = dagger;
            active.SetActive(true);
            Messenger.Broadcast(GameEvent.MELEE_EQUIPPED);
        }

        //Test Button for Taking Damage
        if (Input.GetKeyDown(KeyCode.F1)) {
            OnPlayerHit(10);
        }

        //Player Attack Button Action
        if (Input.GetMouseButton(0)){
            if (active == dagger || active == sword){
                active.GetComponent<MeleeScript>().Swing();
            }
            else if (active == pistol && pistolAmmo > 0){
                
                if (active.GetComponent<Pistol>().Fire()) {
                    pistolAmmo--;
                    Messenger<int>.Broadcast(GameEvent.UPDATE_AMMO, (int)pistolAmmo);
                }
            }
            else if (active == rpg && rpgAmmo > 0) {
                if (active.GetComponent<RPGLauncher>().Fire()){
                    rpgAmmo--;
                    Messenger<int>.Broadcast(GameEvent.UPDATE_AMMO, (int)rpgAmmo);
                }
            }
        }
    }
    //Player picks up Dagger
    public void OnDaggerUnlock() { haveDagger = true; }
    //Player picks up Sword
    public void OnSwordUnlock() { haveSword = true; }
    //Player Picks up Pistol Ammunition, broadcasts for UI Controller
    public void OnPistolAmmoPickup() { 
        pistolAmmo += 12; 
        if(active == pistol){
            Messenger<int>.Broadcast(GameEvent.PISTOL_EQUIPPED, (int)pistolAmmo);
        }
    }

    //Player Picks up RPG Ammunition, Broadcasts to UI
    public void OnRPGAmmoPickup() { 
        rpgAmmo += 5; 
        if(active == rpg){
            Messenger<int>.Broadcast(GameEvent.RPG_EQUIPPED, (int)rpgAmmo);
        }
    }
    //Player unlocks Pistol
    public void OnPistolUnlock() {
        if (havePistol) { OnPistolAmmoPickup(); }
        havePistol = true;
    }
    //Player Unlocks RPG
    public void OnRPGUnlock() {
        if (haveRpg) { OnRPGAmmoPickup(); }
        haveRpg = true;
    }
    //Player Collects Health Kit
    public void OnHealthKitPickup() { 
        if(health + 20 > maxHealth) {
            health = maxHealth;
        } else {
            health += 20;
        }
        Messenger<float>.Broadcast(GameEvent.PLAYER_HEAL, (float)health / maxHealth);
    }
    //Player recieves damage from a source
    public void OnPlayerHit(int damage){
        health -= damage;
        Messenger<float>.Broadcast(GameEvent.PLAYER_HIT, (float)health / maxHealth);
    }
}
