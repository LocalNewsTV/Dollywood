using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] float distanceToActive = 5f;
    [SerializeField] Animator anim;
    private float distanceToAttack = 2.5f;
    private GameObject Player;
    private float health = 10;
    [SerializeField] private EnemyStates state = EnemyStates.idle;
    private float time = 0;
    private Rigidbody rb;
    private int rotateTime = 2;
    private float obstacleRange = 1.5f;
    private float sphereRadius = 1.5f;
    private float attackRange = 1.5f;
    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
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
        active,
        idle,
        attack,
        dead,
        shamble
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);
        rb.AddForce(Vector3.down * 9.81f);
        if((state == EnemyStates.idle || state == EnemyStates.shamble) && distance < distanceToActive){ 
            state = EnemyStates.active;
            anim.SetFloat("speed_f", 6);
        } 
        else if(state == EnemyStates.active){
            //transform.LookAt(Player.transform);

            var lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .05f);

            if (distance < distanceToAttack) { state = EnemyStates.attack; }

        }
        else if (state == EnemyStates.attack){
            var lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.05f);

            rb.velocity = new Vector3(rb.velocity.x,-9.81f,rb.velocity.z);
            anim.SetTrigger("AttackPlayer");
            StartCoroutine(CheckIfHitPlayer());
            
            if(distance > distanceToAttack) { 
                state = EnemyStates.active; 
            }
        }
        else if(state == EnemyStates.shamble)
        { 
            
            anim.SetFloat("speed_f", 1);
            time += Time.deltaTime;
            if(time > rotateTime){
                rotateTime = Random.Range(0, 6);
                time = 0;
            }

        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit) && state != EnemyStates.dead && state != EnemyStates.attack)
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hit.distance < obstacleRange && !hitObject.GetComponent<PlayerCharacter>() && !hitObject.GetComponent<ZombieAI>())
            {
                float turnAngle = Random.Range(-40, 40);
                transform.Rotate(Vector3.up * turnAngle);
            }
        }
        
    }

    public IEnumerator CheckIfHitPlayer(){
        yield return new WaitForSeconds(0.5f);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, sphereRadius, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>() && hit.distance < attackRange && state != EnemyStates.dead)
            {
                Messenger<int>.Broadcast(GameEvent.PLAYER_TAKE_DAMAGE, 5);
                float turnAngle = Random.Range(-110, 110);
                transform.Rotate(Vector3.up * turnAngle);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    public void ChangeState(EnemyStates state){
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
