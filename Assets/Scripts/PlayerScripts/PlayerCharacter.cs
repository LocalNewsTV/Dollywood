using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rpg;
    [SerializeField] private GameObject dagger;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private CharacterController cc;
    private AudioSource sound;
    //Weapon Unlocks
    private bool haveSword;
    private bool havePistol;
    private bool haveRpg;
    private bool haveDagger;

    //Ammunition + HP Variables
    private int health;
    private int pistolAmmo;
    private int rpgAmmo;

    private const int healthKitValue = 20;
    private const int maxHealth = 100;
    private const float invulnerablePeriod = 0.3f;
    private float timeSinceLastHit = 0;

    private bool godmode = false;

    private Vector3 spawnPoint = new Vector3(0, 0, 0);

    private GameObject active; //Currently Active Weapon ref.

    private void Awake() {
        Messenger.AddListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.AddListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger<int>.AddListener(GameEvent.PLAYER_TAKE_DAMAGE, OnPlayerHit);
        Messenger.AddListener(GameEvent.PLAYER_RESPAWN, Respawn);
        Messenger.AddListener(GameEvent.SOUND_CHANGED, AdjustVolume);
        Messenger.AddListener(GameEvent.END_BOSS_FIGHT, OnBossDefeated);
        Messenger.AddListener(GameEvent.NEXT_LEVEL, OnNextLevelLoad);
    }
    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.RemoveListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger<int>.AddListener(GameEvent.PLAYER_TAKE_DAMAGE, OnPlayerHit);
        Messenger.RemoveListener(GameEvent.PLAYER_RESPAWN, Respawn);
        Messenger.RemoveListener(GameEvent.SOUND_CHANGED, AdjustVolume);
        Messenger.RemoveListener(GameEvent.END_BOSS_FIGHT, OnBossDefeated);
        Messenger.RemoveListener(GameEvent.NEXT_LEVEL, OnNextLevelLoad);
    }
    private void OnNextLevelLoad(){
        PlayerPrefs.SetInt(GameTerms.HEALTH, health);
        PlayerPrefs.SetInt(GameTerms.PISTOL_AMMO, pistolAmmo);
        PlayerPrefs.SetInt(GameTerms.RPG_AMMO, rpgAmmo);
    }
    void OnBossDefeated() => godmode = true;
    private void AdjustVolume() { sound.volume = PlayerPrefs.GetInt("sound") / 100.0f; }

    void Start() {
        sound = GetComponent<AudioSource>();
        haveSword = PlayerPrefs.GetInt(GameTerms.SWORD_UNLOCKED, 0) == 1;
        havePistol = PlayerPrefs.GetInt(GameTerms.PISTOL_UNLOCKED, 0) == 1;
        haveRpg = PlayerPrefs.GetInt(GameTerms.RPG_UNLOCKED, 0) == 1;
        haveDagger = PlayerPrefs.GetInt(GameTerms.DAGGER_UNLOCKED, 0) == 1;

        health = PlayerPrefs.GetInt(GameTerms.HEALTH, 100);

        pistolAmmo = PlayerPrefs.GetInt(GameTerms.PISTOL_AMMO, 20);
        rpgAmmo = PlayerPrefs.GetInt(GameTerms.RPG_AMMO, 10);

        active = null;
        AdjustScale(gameObject.transform.localScale.x);
    }
    private void AdjustScale(float scale) {
        Debug.Log(scale);
        sword.GetComponent<MeleeScript>().AdjustScale(scale);
        dagger.GetComponent<MeleeScript>().AdjustScale(scale);
    }
    public void Respawn(){
        if(health <= 0){
            health = maxHealth;
            Messenger<float>.Broadcast(GameEvent.PLAYER_HEAL, (float)health / maxHealth);
        }
        cc.enabled = false;
        gameObject.transform.position = spawnPoint;
        cc.enabled = true;
    }
    public void ChangeSpawnPoint(Vector3 pos){ spawnPoint = pos; }
    public void OnNewGame(){
        PlayerPrefs.SetInt(GameTerms.SWORD_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.PISTOL_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.RPG_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.DAGGER_UNLOCKED, 0);

        PlayerPrefs.SetInt(GameTerms.HEALTH, health);
        PlayerPrefs.SetInt(GameTerms.PISTOL_AMMO, pistolAmmo);
        PlayerPrefs.SetInt(GameTerms.RPG_AMMO, rpgAmmo);

    }
    public void SaveInfo(){
        PlayerPrefs.SetInt(GameTerms.HEALTH, health);
        PlayerPrefs.SetInt(GameTerms.PISTOL_AMMO, pistolAmmo);
        PlayerPrefs.SetInt(GameTerms.RPG_AMMO, rpgAmmo);
    }
    private void OnGameActive(){ this.enabled = true; }
    private void OnGameInactive(){ this.enabled = false; }
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
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
        if (Input.GetKeyDown(KeyCode.F1)) {  OnPlayerHit(10); }
        if (Input.GetKeyDown(KeyCode.F)){flashlight.SetActive( !flashlight.activeSelf); }
        //Player Attack Button Action
        if (Input.GetMouseButton(0)){
            if (active == pistol && (pistolAmmo > 0 || godmode)){
                if (active.GetComponent<Pistol>().Fire() && !godmode) {
                    pistolAmmo--;
                    Messenger<int>.Broadcast(GameEvent.UPDATE_AMMO, (int)pistolAmmo);
                }
            }
            else if (active == rpg && (rpgAmmo > 0 || godmode)) {
                if (active.GetComponent<RPGLauncher>().Fire()){
                    rpgAmmo--;
                    Messenger<int>.Broadcast(GameEvent.UPDATE_AMMO, (int)rpgAmmo);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F9)){ godmode = !godmode; }
    }
    //Player picks up Dagger
    public void OnDaggerUnlock() { 
        haveDagger = true;
        PlayerPrefs.SetInt(GameTerms.DAGGER_UNLOCKED, 1);
    }
    //Player picks up Sword
    public void OnSwordUnlock() { 
        haveSword = true;
        PlayerPrefs.SetInt(GameTerms.SWORD_UNLOCKED, 1);
    }
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
        PlayerPrefs.SetInt(GameTerms.PISTOL_UNLOCKED, 1);
        if (havePistol) { OnPistolAmmoPickup(); }
        havePistol = true;
    }
    //Player Unlocks RPG
    public void OnRPGUnlock() {
        PlayerPrefs.SetInt(GameTerms.RPG_UNLOCKED, 1);
        if (haveRpg) { OnRPGAmmoPickup(); }
        haveRpg = true;
    }
    //Player Collects Health Kit
    public void OnHealthKitPickup() { 
        if(health + healthKitValue > maxHealth) {
            health = maxHealth;
        } else {
            health += healthKitValue;
        }
        Messenger<float>.Broadcast(GameEvent.PLAYER_HEAL, (float)health / maxHealth);
    }
    //Player recieves damage from a source
    public void OnPlayerHit(int damage){
        if (timeSinceLastHit >= invulnerablePeriod && !godmode){
            timeSinceLastHit = 0;
            health -= damage;
            Messenger<float>.Broadcast(GameEvent.PLAYER_HIT, (float)health / maxHealth);
            if(health <= 0){
                Messenger.Broadcast(GameEvent.PLAYER_DIED);
            }
        }
    }

}
