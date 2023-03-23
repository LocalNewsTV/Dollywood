using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    [SerializeField] public float speed = 30f;
    private float bulletRange = 300f;
    private Vector3 init;

    private void Start(){
        init = transform.position;
    }
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, init) > bulletRange){ Destroy(this.gameObject); }
    }

    private void OnTriggerEnter(Collider other){
        ZombieAI enemy = other.gameObject.GetComponent<ZombieAI>();
        BossController boss = other.gameObject.GetComponent<BossController>();
        if (enemy){ enemy.TakeDamage(10);}
        else if (boss) { boss.TakeDamage(10);}
        Destroy(this.gameObject);
        
    }
}
