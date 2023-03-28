using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void AdjustVolume(){ sound.volume = PlayerPrefs.GetInt("sound") / 100.0f; }
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
    private void Update(){
        timeSinceLastFire += Time.deltaTime;
    }
}
