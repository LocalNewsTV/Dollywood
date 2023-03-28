using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private EnemyStates state = EnemyStates.idle; //Default State
    Animator anim;
    private NavMeshAgent agent;
    public GameObject Player;
    private Rigidbody rb;

    //Bool for preventing repeated attacks in small window
    private bool attackInProgress = false;

    //Ranges for Events
    private float distanceToActive = 15f;
    private float obstacleRange = 1.5f;
    private float distanceToAttack = 4f;
    private float attackRange = 4f;
    private float hearingRange = 25f;
    private float sphereRadius = 1.5f;

    //Health Values
    private float health;

    //Calculated distance from Zombie to Player
    private float distance;
   
    //Movement Constants
    private const float gravity = -9.81f;
    private float runSpeed = 12f;
    private float walkSpeed = 1.5f;

    //Sound Keys
    private AudioSource sound;
    [SerializeField] private AudioClip[] sounds;
    private const int sAttackingOne = 0;
    private const int sAttackingTwo = 1;
    private const int sAngryOne = 2;
    private const int sAngryTwo = 3;
    private const int sDamagedOne = 4;
    private const int sDamagedTwo = 5;
    private const int sScreaming = 6;
    /// <summary>
    /// Rolls for a Crit chance at 10% 
    /// </summary>
    /// <returns>
    /// Successful Crit
    /// </returns>
    public bool CalculateCritical()
    {
        return Random.Range(0, 10) == 5;
    }
/// <summary>
/// States for Zombies
/// </summary>
/// 
    public enum EnemyStates{
        idle,
        active,
        attack,
        dead,
        shamble,
        bossAdd,
        reactToHit
    }
    private void Start(){
        rb = GetComponent<Rigidbody>();
        agent.enabled = true; //If left enabled beforehand the zombies will Fly off the map
        anim = GetComponent<Animator>();
        SetSpeed(walkSpeed);
        health = Random.Range(1, 25);
        sound = GetComponent<AudioSource>();
        AdjustVolume();
    }
    private void AdjustVolume(){ sound.volume = PlayerPrefs.GetInt("sound") / 100.0f; }
    private void Awake() { 
        Messenger.AddListener(GameEvent.WEAPON_FIRED, OnWeaponFired);
        Messenger.AddListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    private void OnDestroy(){
        StopAllCoroutines();
        Messenger.RemoveListener(GameEvent.WEAPON_FIRED, OnWeaponFired);
        Messenger.RemoveListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    /// <summary>
    /// Takes in Reference to Game Object, Giving the Zombie a Target to base itself off of
    /// </summary>
    /// <param name="Player">Player Gameobject</param>
    public void SetPlayer(GameObject Player){
        this.Player = Player;
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Called When a Weapon is fired, Alerts Zombie of Player in a greater range
    /// </summary>
    private void OnWeaponFired()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.position) <= hearingRange){
            if(state == EnemyStates.shamble) { StartCoroutine(Scream()); }
            ChangeState(EnemyStates.bossAdd);
            SetSpeed(runSpeed);
            
        }
    }

    private IEnumerator Scream()
    {
        agent.isStopped = true;
        rb.velocity= Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        sound.Stop();
        sound.PlayOneShot(sounds[sScreaming]);
        anim.SetTrigger("Zombie_Scream_Trig");
        yield return new WaitForSeconds(2.8f);
        agent.isStopped = false;
    }
    /// <summary>
    /// Actions Taken for Zombie when In Attack State
    /// </summary>
    private void AttackState(){
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.5f);
        if (!attackInProgress)
        {
            attackInProgress = true;
            agent.SetDestination(gameObject.transform.position);
            rb.velocity = Vector3.up * gravity;
            gameObject.transform.rotation = Quaternion.LookRotation(Player.transform.position - gameObject.transform.position);
            if (AorB()){
                anim.SetTrigger("AttackPlayer_B_trig");
                sound.Stop();
                sound.PlayOneShot(sounds[sAttackingOne]);
                StartCoroutine(CheckIfHitPlayer(4.167f / 2f)); //Animation time cut in half
            } else {
                anim.SetTrigger("AttackPlayer_A_trig");
                sound.Stop();
                sound.PlayOneShot(sounds[sAttackingTwo]);
                StartCoroutine(CheckIfHitPlayer(1f));
            }
            
        }
    }
    /// <summary>
    /// Actions Taken by Zombie when in Attack State
    /// </summary>
    private void ActiveState()
    {
        SetSpeed(runSpeed);
        agent.SetDestination(Player.transform.position);

    }

    /// <summary>
    /// Actions For Zombie when in Shamble State
    /// </summary>
    private void ShambleState()
    {
        SetSpeed(walkSpeed);
        agent.SetDestination(gameObject.transform.position + (gameObject.transform.forward * 2.6f));
    }

    /// <summary>
    /// Sets speed of Zombies animator, and NavMesh Agent 
    /// </summary>
    /// <param name="speed">Speed value</param>
    private void SetSpeed(float speed)
    {
        anim.SetFloat("speed_f", speed);
        agent.speed = speed;
    }

    /// <summary>
    /// Main Zombie Controller, Compares its distance to player to to switch states
    /// Turns when too close to walls
    /// </summary>
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (state == EnemyStates.bossAdd){
            ActiveState();
            if (distance <= distanceToActive) { 
                ChangeState(EnemyStates.active);
            }
        } else {   
            rb.AddForce(Vector3.down * 9.81f);
            if (state != EnemyStates.dead && state != EnemyStates.reactToHit) {
                if (distance <= distanceToAttack) {
                    ChangeState(EnemyStates.attack);
                    AttackState();
                } else if (distance <= distanceToActive){
                    ChangeState(EnemyStates.active);
                    ActiveState();
                } else if (distance > distanceToActive) {
                    ChangeState(EnemyStates.shamble);
                    ShambleState();
                }

            }
            if (state != EnemyStates.dead && state != EnemyStates.attack && state != EnemyStates.reactToHit) {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(ray, sphereRadius, out hit)) {
                    if (hit.distance < obstacleRange) {
                        float turnAngle = Random.Range(-40, 40);
                        transform.Rotate(Vector3.up * turnAngle);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Couldn't find a True or False Random Generator, so shorthanded pulling Ints to this function
    /// </summary>
    /// <returns>Bool at 50% rate</returns>
    private bool AorB(){ return Random.Range(0, 2) == 0; }
    public IEnumerator CheckIfHitPlayer(float animTime){
        agent.isStopped = true;
        agent.SetDestination(gameObject.transform.position);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        float waitPeriod = 0.3f;
        yield return new WaitForSeconds(waitPeriod);
        float distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (distance <= attackRange && state != EnemyStates.dead) {
            Player.GetComponent<PlayerCharacter>().OnPlayerHit(5);
        }
        yield return new WaitForSeconds(animTime - waitPeriod);
        attackInProgress = false;
        agent.isStopped = false;
    }
    //Change State of Zombie AI
    public void ChangeState(EnemyStates state){
        this.state = state;
    }
    /// <summary>
    /// Handler for Zombie Receiving Damage
    /// </summary>
    /// <param name="damage">Amount of HP to deduct</param>
    public void TakeDamage(int damage){
        if (CalculateCritical())
        {
            damage *= 2;
        }
        health -= damage;
        if(health <= 0){
            ChangeState(EnemyStates.dead);
            StopAllCoroutines();
            StartCoroutine(Die());
        } else
        {
            StartCoroutine(ReactToHit());
        }
    }
    /// <summary>
    /// Coroutine for Zombie when Being Hit but not killed
    /// </summary>
    public IEnumerator ReactToHit()
    {
        agent.SetDestination(gameObject.transform.position);
        agent.isStopped = true;
        EnemyStates temp = state;
        
        rb.velocity = Vector3.back;
        rb.angularVelocity = Vector3.zero;
        ChangeState(EnemyStates.reactToHit);
        if (AorB()){
            anim.SetTrigger("React_To_Hit_A_trig");
            sound.Stop();
            sound.PlayOneShot(sounds[sDamagedOne]);
            yield return new WaitForSeconds(2f / 2f);
        } else {
            anim.SetTrigger("React_To_Hit_B_trig");
            sound.Stop();
            sound.PlayOneShot(sounds[sDamagedTwo]);
            yield return new WaitForSeconds(2.167f / 2f);
        }
        ChangeState(temp);
        agent.isStopped = false;
    }
    /// <summary>
    /// Coroutine for When Zombie has run out of HP, Swaps a bunch of components to make its corpse not ragdoll uncontrollably
    /// Calls one of two death animations and deletes itself
    /// </summary>
    private IEnumerator Die(){
        ChangeState(EnemyStates.dead);
        agent.SetDestination(gameObject.transform.position);
        agent.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;

        if(AorB()) {
            anim.SetBool("Dead_A_b", true);
            sound.Stop();
            sound.PlayOneShot(sounds[sDamagedOne]);
            yield return new WaitForSeconds(3); //Based on Time of animation
        } else {
            anim.SetBool("Dead_B_b", true);
            sound.Stop();
            sound.PlayOneShot(sounds[sDamagedTwo]);
            yield return new WaitForSeconds(1); //Based on Time of Animation
        }
        sound.enabled = false;
        Destroy(this.gameObject);   
    }

    public void AdjustScale(float rate){
        runSpeed *= rate;
        walkSpeed *= rate;
        distanceToActive *= rate;
        obstacleRange *= rate;
        distanceToAttack *= rate;
        hearingRange *= rate * 3;
    }
}