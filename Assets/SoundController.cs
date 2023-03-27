using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    public AudioClip[] songs;
    public int bgmIndex;
    private const int bossMusic = 3;
    private const int bossDefeat = 4;
    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.MUSIC_CHANGED, OnMusicChanged);
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.AddListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
    }
    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.MUSIC_CHANGED, OnMusicChanged);
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
        Messenger.RemoveListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
    }

    private void OnStartBossFight()
    {
        bgm.Stop();
        bgm.PlayOneShot(songs[bossMusic]);
    }
    private void OnEndBossFight()
    {
        bgm.Stop();
        bgm.PlayOneShot(songs[bossDefeat]);
    }
    private void OnMusicChanged(int volume)
    {
        bgm.volume = volume / 100.0f;
    }


    void Start()
    {
        bgm.volume = PlayerPrefs.GetInt("music") / 100.0f;
        bgm.PlayOneShot(songs[bgmIndex]);
    }
}
