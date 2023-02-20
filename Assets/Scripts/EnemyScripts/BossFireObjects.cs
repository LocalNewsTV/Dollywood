using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireObjects : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject defaultObj;
    [SerializeField] float ObjectScale;
    [SerializeField] ProjectileLibrary pl;
    [SerializeField] BossController bc;
    [SerializeField] string partName;
    private GameObject ammunition;
    private Vector3 big = new Vector3(4, 4, 4);

    public void FireRandom()
    {
        ammunition = Instantiate(pl.SelectItem()) as GameObject;
        StartCoroutine(LaunchItem());
    }

    public void Fire()
    {
        ammunition = Instantiate(defaultObj) as GameObject;
        StartCoroutine(LaunchItem());
    }
    private IEnumerator LaunchItem(){
        ammunition.transform.position = transform.TransformPoint(Vector3.forward);
        ammunition.transform.LookAt(Player.transform);
        yield return new WaitForSeconds(0.1f);
        if (ammunition)
        {
            ammunition.transform.localScale = big;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int damage = 0;
        if (other.CompareTag("RPG")) { damage = 50; }
        else if (other.CompareTag("bullet")) { damage = 10; }
        else if (other.CompareTag("PlayerMelee")) { damage = 8; }
        if(damage > 0) { bc.TakeDamage(damage, partName); }
        
    }
}
