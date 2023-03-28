using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent{
    //Example to Copy/paste
    public const string eventA = "eventA";
    //World Events
    public const string OPEN_EXIT_DOOR = "OPEN_EXIT_DOOR";
    public const string REVEAL_PHOTOS = "REVEAL_PHOTOS";
    
    //Enemy Spawning Calls
    public const string ENEMY_SPAWN_A = "ENEMY_SPAWN_A";
    public const string ENEMY_SPAWN_B = "ENEMY_SPAWN_B";
    public const string ENEMY_SPAWN_C = "ENEMY_SPAWN_C";
    public const string ENEMY_SPAWN_D = "ENEMY_SPAWN_D";
    //Enemy Events
    public const string ENEMY_HIT = "ENEMY_HIT";
    public const string START_BOSS_FIGHT = "START_BOSS_FIGHT";
    public const string END_BOSS_FIGHT = "END_BOSS_FIGHT";
    public const string ENEMY_DEAD = "ENEMY_DEAD";
    public const string BOSS_SPAWN_ONE = "BOSS_SPAWN_ONE";
    public const string BOSS_SPAWN_ALL = "BOSS_SPAWN_ALL";

    //Menus
    public const string POPUP_OPENED = "POPUP_OPENED";
    public const string POPUP_CLOSED = "POPUP_CLOSED";

    public const string GAME_INACTIVE = "GAME_INACTIVE";
    public const string GAME_ACTIVE = "GAME_ACTIVE";

    //Settings
    public const string SOUND_CHANGED = "SOUND_CHANGED";
    public const string SOUND_OFF = "SOUND_OFF";
    public const string SOUND_ON = "SOUND_ON";
    
    public const string MUSIC_CHANGED = "MUSIC_CHANGED";
    public const string MUSIC_OFF = "MUSIC_OFF";
    public const string MUSIC_ON = "MUSIC_ON";

    public const string SENSITIVITY_CHANGED = "SENSITIVITY_CHANGED";

    public const string DISABLE_CROSSHAIR = "DISABLE_CROSSHAIR";
    public const string ENABLE_CROSSHAIR = "ENABLE_CROSSHAIR";
    public const string NEXT_LEVEL = "NEXT_LEVEL";
    public const string NEW_GAME = "NEW_GAME";

    //Weapon Unlocks
    public const string DAGGER_UNLOCK = "DAGGER_UNLOCK";
    public const string SWORD_UNLOCK = "SWORD_UNLOCK";
    public const string PISTOL_UNLOCK = "PISTOL_UNLOCK";
    public const string RPG_UNLOCK = "RPG_UNLOCK";
    public const string GODMODE_PRESSED = "GODMODE_PRESSED";

    //Pickups
    public const string PISTOL_AMMO_PICKUP = "PISTOL_AMMO_PICKUP";
    public const string RPG_AMMO_PICKUP = "RPG_AMMO_PICKUP";
    public const string HEALTH_KIT_PICKUP = "HEALTH_KIT_PICKUP";

    
    //Player
    public const string PLAYER_HIT = "PLAYER_HIT";
    public const string PLAYER_TAKE_DAMAGE = "PLAYER_TAKE_DAMAGE";
    public const string PLAYER_HEAL = "PLAYER_HEAL";
    public const string CHANGE_SPAWN_POINT = "CHANGE_SPAWN_POINT";
    public const string PLAYER_DIED = "PLAYER_DIED";
    public const string PLAYER_RESPAWN = "PLAYER_RESPAWN";

    //UI Events
    public const string PISTOL_EQUIPPED = "PISTOL_EQUIPPED";
    public const string RPG_EQUIPPED = "RPG EQUIPPED";
    public const string MELEE_EQUIPPED = "MELEE_EQUIPPED";
    public const string UPDATE_AMMO = "UPDATE_AMMO";
    public const string FADE_OUT = "FADE_OUT";

    //Map Traversing
    public const string RETURN_TO_MAIN_MENU = "RETURN_TO_MAIN_MENU";
    public const string RESTART_CURRENT_MAP = "RESTART_CURRENT_MAP";
    public const string UNLOCK_EXIT = "UNLOCK_EXIT";
    public const string UNLOCK_DOOR_A = "UNLOCK_DOOR_A";
    public const string UNLOCK_DOOR_B = "UNLOCK_DOOR_B";
    public const string UNLOCK_DOOR_C = "UNLOCK_DOOR_C";
    public const string UNLOCK_DOOR_D = "UNLOCK_DOOR_D";

    public const string EXPLOSION = "EXPLOSION";
    public const string WEAPON_FIRED = "WEAPON_FIRED";
    public const string TIP_RECEIVED = "TIP_RECEIVED";
}
