using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : BasePopup
{
    [SerializeField] private OptionsPopup optionsPopup;

    [SerializeField] private TextMeshProUGUI musicLabel;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private TextMeshProUGUI soundLabel;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private TextMeshProUGUI mouseSensitivityLabel;
    [SerializeField] private Slider mouseSensitivitySlider;

    private void Start()
    {
        UseSavedGameSettings();
    }

    public void UseSavedGameSettings(){
        musicSlider.value = PlayerPrefs.GetInt("music", 50);
        UpdateMusic(musicSlider.value);

        soundSlider.value = PlayerPrefs.GetInt("sound", 50);
        UpdateSound(soundSlider.value);

        mouseSensitivitySlider.value = PlayerPrefs.GetInt("sensitivity", 50);
        UpdateSensitivity(mouseSensitivitySlider.value);
    }
    override public void Open(){
        base.Open();
    }
    override public void Close(){
        optionsPopup.Open();
        base.Close();
    }
    public void OnSettingsCancel(){
        UseSavedGameSettings();
        Close();
    }
    public void OnSettingsOK(){
        PlayerPrefs.SetInt("music", (int)musicSlider.value);
        PlayerPrefs.SetInt("sound", (int)soundSlider.value);
        PlayerPrefs.SetInt("sensitivity", (int)mouseSensitivitySlider.value);

        Messenger<int>.Broadcast(GameEvent.MUSIC_CHANGED, (int)musicSlider.value);
        //Messenger<int>.Broadcast(GameEvent.SOUND_CHANGED, (int)soundSlider.value);
        Messenger<int>.Broadcast(GameEvent.SENSITIVITY_CHANGED, (int)mouseSensitivitySlider.value);
        Close();
    }

    public void UpdateMusic(float music){
        musicLabel.text = music.ToString();
    }
    public void UpdateSound(float sound){
        soundLabel.text = sound.ToString();
    }
    public void UpdateSensitivity(float sensitivity){
        mouseSensitivityLabel.text = sensitivity.ToString();
    }

  
    public void OnMusicValueChanges(float difficulty){
        UpdateMusic(difficulty);
    }
    public void OnSoundValueChanges(float difficulty){
        UpdateSound(difficulty);
    }
    public void OnSensitivityValueChanges(float difficulty){
        UpdateSensitivity(difficulty);
    }
}
