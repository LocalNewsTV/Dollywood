using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] float distanceToActive = 5f;
    [SerializeField] Animator anim;
    private float distanceToAttack = 1f;
    private GameObject Player;
    private float health = 10;
    [SerializeField]private EnemyStates state = EnemyStates.idle;
    private float time = 0;

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
        shamble
    }

    
    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if((state == EnemyStates.idle || state == EnemyStates.shamble) && distance < distanceToActive){ 
            state = EnemyStates.active;
            anim.SetFloat("speed_f", 6);
        } 
        else if(state == EnemyStates.active){
            transform.LookAt(Player.transform);
            if (distance < distanceToAttack) { state = EnemyStates.attack; }

        }
        else if (state == EnemyStates.attack){
            anim.SetTrigger("AttackPlayer");
            if(distance > distanceToAttack) { 
                state = EnemyStates.active; 
            }
        }
        else if(state == EnemyStates.shamble)
        {
            anim.SetFloat("speed_f", 1);
            time += Time.deltaTime;
            if(time > 2)
            {
                time = 0;
                transform.Rotate(new Vector3(0, Random.Range(-90, 90), 0));
            }

        }
        
    }

    public void ChangeState(EnemyStates state)
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
        anim.SetFloat("speed_f", 6);
        state = EnemyStates.active;
    }
    private IEnumerator Die()
    {
        anim.SetBool("alive_b", false);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        
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
