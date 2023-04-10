using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sound;
    public AudioClip[] songs;
    public int bgmIndex;
    private const int bossMusic = 3;
    private const int bossDefeat = 4;
    private void Awake(){
        Messenger.AddListener(GameEvent.MUSIC_CHANGED, AdjustVolume);
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.AddListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
        Messenger.AddListener(GameEvent.EXPLOSION, OnExplosion);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.MUSIC_CHANGED, AdjustVolume);
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.RemoveListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
        Messenger.AddListener(GameEvent.EXPLOSION, OnExplosion);
    }

    //Play the explosion sound through the main speaker
    private void OnExplosion(){ sound.Play(); }
    //Enable the boss music at the start of boss battle
    private void OnStartBossFight(){
        bgm.Stop();
        bgm.PlayOneShot(songs[bossMusic]);
    }
    //Swap songs upon defeat of the boss
    private void OnEndBossFight(){
        bgm.Stop();
        bgm.PlayOneShot(songs[bossDefeat]);
    }
    //Adjust volume to what is stored in Player Preferences
    private void AdjustVolume(){ 
        bgm.volume = PlayerPrefs.GetInt("music") / 100.0f;
        sound.volume = PlayerPrefs.GetInt("sound") / 100.0f;
    }
    //Start method, Adjust the volume and play the song matching the level
    void Start(){
        AdjustVolume();
        bgm.PlayOneShot(songs[bgmIndex]);
    }
}
