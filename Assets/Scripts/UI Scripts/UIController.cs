using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject hud;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private TextMeshProUGUI ammoValueLabel;

    [SerializeField] private GameObject credits;
    [SerializeField] private YouDiedPopup deathPopup;
    [SerializeField] private TextMeshProUGUI tipScreen1;
    [SerializeField] private TextMeshProUGUI tipScreen2;
    [SerializeField] private TextMeshProUGUI tipScreen3;

    [SerializeField] private Image[] screenIndicator;
    private string blank = "";
    private int popupsActive = 0;
    // Start is called before the first frame update

    private void Awake(){
        //Menu UI
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.AddListener(GameEvent.DISABLE_CROSSHAIR, OnPopupClosed);
        Messenger.AddListener(GameEvent.ENABLE_CROSSHAIR, OnPopupClosed);
        //Health
        Messenger<float>.AddListener(GameEvent.PLAYER_HIT, OnPlayerDamage);
        //Weapons
        Messenger<int>.AddListener(GameEvent.PISTOL_EQUIPPED, UpdateAmmo);
        Messenger<int>.AddListener(GameEvent.RPG_EQUIPPED, UpdateAmmo);
        Messenger.AddListener(GameEvent.MELEE_EQUIPPED, UpdateAmmoForMelee);
        Messenger<int>.AddListener(GameEvent.UPDATE_AMMO, UpdateAmmo);
        Messenger.AddListener(GameEvent.RPG_AMMO_PICKUP, OnPickupAmmo);
        Messenger.AddListener(GameEvent.PISTOL_AMMO_PICKUP, OnPickupAmmo);
        Messenger.AddListener(GameEvent.HEALTH_KIT_PICKUP, OnHealthkitPickup);
        Messenger<float>.AddListener(GameEvent.PLAYER_HEAL, OnPlayerHeal);
        Messenger.AddListener(GameEvent.NEXT_LEVEL, OnFadeOut);
        Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDied);
        Messenger.AddListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.DISABLE_CROSSHAIR, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.ENABLE_CROSSHAIR, OnPopupClosed);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_HIT, OnPlayerDamage);
        Messenger<int>.RemoveListener(GameEvent.PISTOL_EQUIPPED, UpdateAmmo);
        Messenger<int>.RemoveListener(GameEvent.RPG_EQUIPPED, UpdateAmmo);
        Messenger.RemoveListener(GameEvent.MELEE_EQUIPPED, UpdateAmmoForMelee);
        Messenger<int>.RemoveListener(GameEvent.UPDATE_AMMO, UpdateAmmo);
        Messenger.RemoveListener(GameEvent.RPG_AMMO_PICKUP, OnPickupAmmo);
        Messenger.RemoveListener(GameEvent.PISTOL_AMMO_PICKUP, OnPickupAmmo);
        Messenger.RemoveListener(GameEvent.HEALTH_KIT_PICKUP, OnHealthkitPickup);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_HEAL, OnPlayerHeal);
        Messenger.RemoveListener(GameEvent.NEXT_LEVEL, OnFadeOut);
        Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDied);
        Messenger.RemoveListener(GameEvent.END_BOSS_FIGHT, OnEndBossFight);
    }
    private void OnEndBossFight()
    {
        hud.SetActive(false);
        credits.SetActive(true);
        credits.GetComponent<Animation>().Play();
        StartCoroutine(EndGame());
    }
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(60);
        Messenger.Broadcast(GameEvent.RETURN_TO_MAIN_MENU);
    }
    public void OnTipReceived(string tip){
        StartCoroutine(DisplayTip(tip));
    }
    private IEnumerator DisplayTip(string tip)
    {
        TextMeshProUGUI tipTarget;
        if(tipScreen1.text == "") { tipTarget = tipScreen1; }
        else if (tipScreen2.text == "") { tipTarget = tipScreen2; }
        else if (tipScreen3.text == "") { tipTarget = tipScreen3; }
        else { tipTarget = tipScreen1; }
        tipTarget.text = tip;
        yield return new WaitForSeconds(5);
        tipTarget.text = blank;
    }
    private void OnFadeOut(){
        StartCoroutine(FadeToBlack());
    }
    private void OnPlayerDied()
    {
        deathPopup.Open();
        //deathPopup.PlayerDied();
    }
    private IEnumerator FadeToBlack(){
        Color black = Color.black;
        foreach (Image indic in screenIndicator){
            indic.color = Color.black;
        }
        for (int i = 0; i < 75; i++)
        {
            black.a += 0.01f;
            foreach (Image indic in screenIndicator)
            {
                indic.color = black;
            }
            yield return new WaitForSeconds(0.08f);
        }
    }
    private void OnPlayerHeal(float percent){
        UpdateHealthIcon(percent);
    }
    private void OnPickupAmmo(){
        StartCoroutine(IndicateEvent(Color.blue));
    }
    private void OnHealthkitPickup(){
        StartCoroutine(IndicateEvent(Color.green));
    }
    private IEnumerator IndicateEvent(Color color)
    {
        color.a = 0;
        foreach(Image indic in screenIndicator){
            indic.color = color;
        }
        for (int i = 0; i < 50; i++) {
            color.a += 0.01f;
            foreach (Image indic in screenIndicator) { 
            indic.color = color;
            }
            yield return new WaitForSeconds(0.0015f);
        }
        for(int i = 0; i < 50; i++)
        {
            color.a -= 0.01f;
            foreach (Image indic in screenIndicator) { 
                indic.color = color;
            }
            yield return new WaitForSeconds(0.0015f);
        }
    }
    private void OnPopupOpened(){
        popupsActive++;
        if (popupsActive > 0){
            SetGameActive(false);   
        }
    }
    private void OnPopupClosed(){
        popupsActive--;
        if (popupsActive == 0){
            SetGameActive(true);
        }
    }
    void Start(){
        UpdateHealthIcon(1f);
        UpdateAmmoForMelee();
        SetGameActive(true);
    }
    private void UpdateAmmo(int ammo){
        ammoValueLabel.text = ammo.ToString();
    }
    private void UpdateAmmoForMelee(){
        ammoValueLabel.text = "Infinite";
    }
    public void UpdateHealthIcon(float percent){
        healthBar.fillAmount = percent;
    }
    public void OnPlayerDamage(float percent){
        StartCoroutine(IndicateEvent(Color.red));
        UpdateHealthIcon(percent);
    }
    public void SetGameActive(bool active)
    {
        if(active)
        {
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crossHair.gameObject.SetActive(true);
        } else
        {
            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossHair.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && popupsActive == 0){
            optionsPopup.Open();
        }
    }
}
