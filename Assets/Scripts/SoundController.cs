using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sound;
    public AudioClip[] songs;
    public int bgmIndex;
    private const int bossMusic = 3;
    private const int bossDefeat = 4;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.MUSIC_CHANGED, AdjustVolume);
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.AddListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
        Messenger.AddListener(GameEvent.EXPLOSION, OnExplosion);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.MUSIC_CHANGED, AdjustVolume);
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.RemoveListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
        Messenger.AddListener(GameEvent.EXPLOSION, OnExplosion);
    }

    private void OnExplosion(){ sound.Play(); }
    private void OnStartBossFight(){
        bgm.Stop();
        bgm.PlayOneShot(songs[bossMusic]);
    }
    private void OnEndBossFight(){
        bgm.Stop();
        bgm.PlayOneShot(songs[bossDefeat]);
    }
    private void AdjustVolume(){ 
        bgm.volume = PlayerPrefs.GetInt("music") / 100.0f;
        sound.volume = PlayerPrefs.GetInt("sound") / 100.0f;
    }

    void Start(){
        AdjustVolume();
        bgm.PlayOneShot(songs[bgmIndex]);
    }
}
