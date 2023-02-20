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
    private Rigidbody rb;
    private float speed = 7f;

    private float gravity = -9.8f;
    private float yVelocity = 0.0f;
    private float groundedYVelocity = -4.0f;
    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
        rb = GetComponent<Rigidbody>();
    }
    public enum EnemyStates
    {
        active,
        idle,
        attack,
        dead
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Player.transform.position);
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        Debug.Log(distance);
        if(state == EnemyStates.idle && distance < distanceToActive){ state = EnemyStates.active;} 
        else if(state == EnemyStates.active){
            Debug.Log("I am in Active Mode");
            transform.LookAt(Player.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if(distance < distanceToAttack) { state = EnemyStates.attack; }

        }
        else if (state == EnemyStates.attack){
            if(distance > distanceToAttack) { state = EnemyStates.active; }
            Debug.Log("I am in Attack Mode");
        }
        
    }

  
    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
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
            TakeDamage(2);
        }
        else if (other.CompareTag("PlayerMelee"))
        {
            TakeDamage(9);
        }
    }

}
