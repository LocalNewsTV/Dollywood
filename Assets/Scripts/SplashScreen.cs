using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
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
