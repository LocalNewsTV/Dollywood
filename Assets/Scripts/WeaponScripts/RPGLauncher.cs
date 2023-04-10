using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for the Rocket Propelled Grenade Launcher
/// </summary>
public class RPGLauncher : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.3f;
    private FireWeapon fw;
    private float timeSinceLastFire = 0;
    private AudioSource sound;
    // Start is called before the first frame update

    private void Start(){
        fw = GetComponent<FireWeapon>();
        sound = GetComponent<AudioSource>();
        AdjustVolume();
    }

    private void Awake(){
        Messenger.AddListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    //Adjusts volume of weapon to be what the players settings are
    private void AdjustVolume(){ sound.volume = PlayerPrefs.GetInt("sound") / 100.0f; }
    /// <summary>
    /// Firing action for Rocket Launcher, resets and Plays sound so they don't overlap, resets timer for firing rate
    /// </summary>
    /// <returns>Boolean if firing weapon was a success</returns>
    public bool Fire(){
        if (timeSinceLastFire >= fireRate){
            sound.Stop();
            sound.Play();
            timeSinceLastFire = 0;
            fw.FireAmmo();
            return true;
        }
        return false;
    }
    //Updates the Firing rate timer for consistency
    private void Update(){
        timeSinceLastFire += Time.deltaTime;
    }
}
