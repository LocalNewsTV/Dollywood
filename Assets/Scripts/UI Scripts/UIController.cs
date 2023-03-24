using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
 

    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private TextMeshProUGUI ammoValueLabel;
    [SerializeField] private GameObject screenIndicator;

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
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.DISABLE_CROSSHAIR, OnPopupClosed);
        Messenger.RemoveListener(GameEvent.ENABLE_CROSSHAIR, OnPopupClosed);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_HIT, UpdateHealthIcon);
        Messenger<int>.RemoveListener(GameEvent.PISTOL_EQUIPPED, UpdateAmmo);
        Messenger<int>.RemoveListener(GameEvent.RPG_EQUIPPED, UpdateAmmo);
        Messenger.RemoveListener(GameEvent.MELEE_EQUIPPED, UpdateAmmoForMelee);
        Messenger<int>.RemoveListener(GameEvent.UPDATE_AMMO, UpdateAmmo);
        Messenger.RemoveListener(GameEvent.RPG_AMMO_PICKUP, OnPickupAmmo);
        Messenger.RemoveListener(GameEvent.PISTOL_AMMO_PICKUP, OnPickupAmmo);
        Messenger.RemoveListener(GameEvent.HEALTH_KIT_PICKUP, OnHealthkitPickup);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_HEAL, OnPlayerHeal);
        Messenger.RemoveListener(GameEvent.NEXT_LEVEL, OnFadeOut);
    }
    private void OnFadeOut(){
        StartCoroutine(FadeToBlack());
    }
    private IEnumerator FadeToBlack(){
        Image img = screenIndicator.GetComponent<Image>();
        Color black = Color.black;
        img.color = Color.black;
        for (int i = 0; i < 75; i++)
        {
            black.a += 0.01f;
            img.color = black;
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
        Image img = screenIndicator.GetComponent<Image>();
        color.a = 0;
        img.color = color;
        for(int i = 0; i < 50; i++) {
            color.a += 0.01f;
            img.color = color;
            yield return new WaitForSeconds(0.0015f);
        }
        for(int i = 0; i < 50; i++)
        {
            color.a -= 0.01f;
            img.color = color;
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
        Debug.Log(ammoValueLabel.text);
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
