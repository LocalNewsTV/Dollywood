using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    public AudioClip[] songs;
    public int bgmIndex;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.MUSIC_CHANGED, OnMusicChanged);
   
    }
    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.MUSIC_CHANGED, OnMusicChanged);
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
