using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private EnemyStates state = EnemyStates.idle;
    [SerializeField] Animator anim;
    private NavMeshAgent agent;

    private float distanceToActive = 15f;
    private GameObject Player;
    private float health = 10;
    
    private float time = 0;
    private Rigidbody rb;
    private int rotateTime = 2;
    private float obstacleRange = 1.5f;
    private float sphereRadius = 1.5f;
    private float distanceToAttack = 3f;
    private float attackRange = 2.75f;
    private float distance;
    private bool attackInProgress = false;
    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //set colour
        //Determine the range vector (Starting a enemy)
        Vector3 rangeTest = transform.position + transform.forward * attackRange;
        //Draw line to show the range vector
        Debug.DrawLine(transform.position, rangeTest);
        //draw a wire sphere at the point on the end of the range vector;
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    public enum EnemyStates {
        idle,
        active,
        attack,
        dead,
        shamble
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent.enabled= true;
    }

    private void AttackState()
    {
        rb.velocity = new Vector3(rb.velocity.x, -9.81f, rb.velocity.z);
        Debug.Log("attacking");
        anim.SetTrigger("AttackPlayer");
        if (!attackInProgress){
            attackInProgress = true;
            StartCoroutine(CheckIfHitPlayer());
        }
    }
    private void ActiveState()
    {
        anim.SetFloat("speed_f", 6);
        agent.SetDestination(Player.transform.position);

    }

    private void ShambleState()
    {
        anim.SetFloat("speed_f", 1);
        time += Time.deltaTime;
        if (time > rotateTime)
        {
            rotateTime = Random.Range(0, 6);
            time = 0;
        }
    }
    private void EnemyLookAtPlayer() {
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .05f);
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        rb.AddForce(Vector3.down * 9.81f);

        if (state != EnemyStates.dead) {
            if (distance <= distanceToAttack) {
                ChangeState(EnemyStates.attack);
                AttackState();
            }
            else if (distance <= distanceToActive) { 
                ChangeState(EnemyStates.active);
                ActiveState();
            }
            else if (distance > distanceToActive) { 
                ChangeState(EnemyStates.shamble); 
                ShambleState();
            }
           
        }
        if (state != EnemyStates.dead && state != EnemyStates.attack)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-40, 40);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        }
    }

    public IEnumerator CheckIfHitPlayer(){
        yield return new WaitForSeconds(0.5f);
        if(Vector3.Distance(Player.transform.position, this.transform.position) <= attackRange) {
                Messenger<int>.Broadcast(GameEvent.PLAYER_TAKE_DAMAGE, 5);
        }
        yield return new WaitForSeconds(0.5f);
        attackInProgress = false;
    }
    public void ChangeState(EnemyStates state){
        this.state = state;
    }
    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            StartCoroutine(Die());
        }
    }


    public void SetToKill(){
        anim.SetFloat("speed_f", 6);
        state = EnemyStates.active;
    }
    private IEnumerator Die(){
        anim.SetBool("alive_b", false);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        
    }
    //private void OnTriggerEnter(Collider other){
    //    if (other.CompareTag("bullet")){
    //        TakeDamage(10);
    //    }
    //    else if (other.CompareTag("PlayerMelee"))
    //    {
    //        TakeDamage(9);
    //    }
    //    else if (other.CompareTag("RPG"))
    //    {
    //        TakeDamage(50);
    //    }
    //}
}
