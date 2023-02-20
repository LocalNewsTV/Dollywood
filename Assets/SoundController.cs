using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject friendInJesus;
    [SerializeField] private GameObject darkColossus;
    [SerializeField] private GameObject alwaysLoveYou;
    GameObject currSong;
    void Start()
    {
        currSong = friendInJesus;
        currSong.SetActive(true);   
    }

    public void DarkColossus()
    {
        currSong.SetActive(false);
        currSong = darkColossus;
        currSong.SetActive(true);
    }

    public void AlwaysLoveYou()
    {
        currSong.SetActive(false);
        currSong = alwaysLoveYou;
        currSong.SetActive(true);
    }
}
