using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script loaded in for the splash menu of the game, that promps a player to press Enter to get started
/// Script sets all the playerprefs to their default value to give the feeling of a new game
/// </summary>
public class SplashScreen : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt(GameTerms.SWORD_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.PISTOL_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.RPG_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.DAGGER_UNLOCKED, 0);
        PlayerPrefs.SetInt(GameTerms.HEALTH, 100);
        PlayerPrefs.SetInt(GameTerms.PISTOL_AMMO, 20);
        PlayerPrefs.SetInt(GameTerms.RPG_AMMO, 10);
    }

    void Update(){
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)){
            SceneManager.LoadScene("Area1");
        }
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
