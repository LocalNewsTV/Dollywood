using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform eye1;
    [SerializeField] private Transform eye2;
    [SerializeField] private Transform handLeft;
    [SerializeField] private Transform handRight;
    [SerializeField] private GameObject hands;
    [SerializeField] private BossMovement bm;
    [SerializeField] GameObject[] projectileList;
    [SerializeField] GameObject Player;

    private const int missiles = 1;
    private const int lasers = 0;
    private Vector3 projScale = new Vector3(5, 5, 5);
    public enum EnemyStates { 
        quarterHP,
        halfHP, 
        passive, 
        awakened, 
        dead 
    }

    private EnemyStates state = EnemyStates.passive;
    private int health;
    private float timeBetweenActions = 5f;
    private float timeToAction = 0;
    private float speedMin = 10;
    private float speedMax = 31;
    // Start is called before the first frame update

    void Awake(){
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }
    void Start(){
        health = 10;
    }

    private IEnumerator Die()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        //sound.AlwaysLoveYou();
        for(int i = 0; i < 11; i++){
            mr.enabled = !mr.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(this.gameObject);
        
    }
    public void TakeDamage(int damage){
        if (state != EnemyStates.passive){
            health -= damage;
            if (health <= 0 && state != EnemyStates.dead){
                state = EnemyStates.dead;
                StartCoroutine(Die());
            }
        }
    }
    // Update is called once per frame
    void Update(){
        if(state != EnemyStates.passive) { 
            timeToAction += Time.deltaTime;
            if (timeToAction >= timeBetweenActions){
                timeToAction = 0;
                BossAction();
            }
        }
    }

    public void OnStartBossFight(){
        hands.SetActive(true);
        state = EnemyStates.awakened;
    }

    void BossAction(){
        if (Random.Range(0, 20) == 3) { Messenger.Broadcast(GameEvent.BOSS_SPAWN_ALL); }
        else
        {
            float action = Random.Range(1, 6);
            int rounds = 0;
            switch (action)
            {
                case 1:
                    rounds = Random.Range(1, 8);
                    StartCoroutine(ShootLasers(rounds));
                    break;
                case 2:
                    rounds = Random.Range(1, 4);
                    StartCoroutine(ShootRockets(rounds));
                    break;
                case 3:
                    bm.ChangeSpeed(Random.Range(speedMin, speedMax));
                    break;
                case 4:
                    rounds = Random.Range(3, 5);
                    StartCoroutine(ShootRandom(rounds));
                    break;
                case 5:
                    rounds = Random.Range(1, 3);
                    StartCoroutine(SummonAdds(rounds));
                    break;
            }
        }
    }
    private IEnumerator ShootLasers(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            if (eye1) { FireLasers(eye1); }
            yield return new WaitForSeconds(0.3f);
            if (eye2) { FireLasers(eye2); }
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator ShootRockets(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            if (handLeft) { FireRockets(handLeft); }
            yield return new WaitForSeconds(0.3f);
            if (handRight) { FireRockets(handRight); }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator ShootRandom(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            if (handLeft) { FireRandom(handLeft); }
            yield return new WaitForSeconds(0.3f);
            if (handRight) { FireRandom(handRight); }
            yield return new WaitForSeconds(0.3f);
            if(!handRight && !handLeft) { StartCoroutine(ShootLasers(10)); }
        }
    }

    private IEnumerator SummonAdds(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            Messenger.Broadcast(GameEvent.BOSS_SPAWN_ONE);
            yield return new WaitForSeconds(0.5f);
            Messenger.Broadcast(GameEvent.BOSS_SPAWN_ONE);
            yield return new WaitForSeconds(0.5f);
        }
    }



    public void FireRandom(Transform pos) {
        GameObject ammunition = Instantiate(projectileList[Random.Range(0, projectileList.Length)]);
        StartCoroutine(LaunchItem(ammunition, pos));
    }

    public void FireLasers(Transform pos) {
        GameObject ammunition = Instantiate(projectileList[lasers]);
        StartCoroutine(LaunchItem(ammunition, pos));
    }
    public void FireRockets(Transform pos) {
        GameObject ammunition = Instantiate(projectileList[missiles]);
        StartCoroutine(LaunchItem(ammunition, pos));
    }
    private IEnumerator LaunchItem(GameObject ammunition, Transform pos) {
        ammunition.transform.position = pos.transform.TransformPoint(Vector3.forward);
        ammunition.transform.LookAt(Player.transform);
        yield return new WaitForSeconds(0.1f);
        if (ammunition) {
            ammunition.transform.localScale = projScale;
        }
    }
}
