using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] float distanceToActive = 5f;
    private float distanceToAttack = 0.5f;
    private GameObject Player;
    private float health = 10;
    private EnemyStates state = EnemyStates.idle;
    private float speed = 7f;

    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
    }
    public enum EnemyStates
    {
        active,
        idle,
        attack,
        dead,
        wander
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Player.transform.position);
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        Debug.Log(distance);
        if(state == EnemyStates.idle && distance < distanceToActive){ state = EnemyStates.active;} 
        else if(state == EnemyStates.active){
            transform.LookAt(Player.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if(distance < distanceToAttack) { state = EnemyStates.attack; }

        }
        else if (state == EnemyStates.attack){
            if(distance > distanceToAttack) { state = EnemyStates.active; }
        }
        
    }

    void ChangeState(EnemyStates state)
    {
        this.state = state;
    }
    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void SetToKill()
    {
        state = EnemyStates.active;
    }
    private IEnumerator Die()
    {
        Debug.Log("Enemy Dying");
        Destroy(this.gameObject);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            TakeDamage(10);
        }
        else if (other.CompareTag("PlayerMelee"))
        {
            TakeDamage(9);
        }
        else if (other.CompareTag("RPG"))
        {
            TakeDamage(50);
        }
    }

}
