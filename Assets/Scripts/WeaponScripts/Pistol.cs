using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject casing;
    [SerializeField] private Transform spawnPos;
    private AudioSource sound;
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private float fireRate = 0.3f;
    private FireWeapon fw;
    private float timeSinceLastFire = 0;
    private GameObject casingref;
    // Start is called before the first frame update

    private void Awake(){
        Messenger.AddListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    private void AdjustVolume() {
        sound.volume = PlayerPrefs.GetInt("sound") / 100.0f;
    }
    private void Start()
    {
        fw = GetComponent<FireWeapon>();
        sound = GetComponent<AudioSource>();
        AdjustVolume();
    }
    public bool Fire()
    {
        if (timeSinceLastFire >= fireRate){
            sound.Stop();
            sound.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            timeSinceLastFire = 0;
            fw.FireAmmo();
            EjectShellFromPistol();
            return true;
        }
        return false;
    }
    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }
    void EjectShellFromPistol()
    {
            casingref = Instantiate(casing) as GameObject;
            casingref.transform.position = spawnPos.TransformPoint(0, 0, 0);
            casingref.transform.rotation = transform.rotation;
            casingref.GetComponent<Rigidbody>().AddForce(10, 5, 0);
    }
}
