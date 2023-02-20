using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rpg;
    [SerializeField] private GameObject dagger;
    [SerializeField] private GameObject sword;
    private bool haveSword = true;
    private bool havePistol = true;
    private bool haveRpg = true;
    private bool haveDagger = true;
    
    private GameObject active;
    void Start()
    {
        health = 5;
        active = null;
    }
    public void Hit()
    {
        health -= 1;
        Debug.Log("Health: " + health);
        if (health == 0)
        {
            Debug.Break();
        }
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetButtonDown("RPG") && haveRpg)
        {
            if (active) { active.SetActive(false); }
            active = rpg;
            active.SetActive(true);
        }
        else if (Input.GetButtonDown("Pistol") && havePistol)
        {
            if (active) { active.SetActive(false); }
            active = pistol;
            active.SetActive(true);
        }
        else if (Input.GetButtonDown("Sword") && haveSword)
        {
            if (active) { active.SetActive(false); }
            active = sword;
            active.SetActive(true);
        }
        else if (Input.GetButtonDown("Dagger") && haveDagger)
        {
            if (active) { active.SetActive(false); }
            active = dagger;
            active.SetActive(true);
        }
    }
}
