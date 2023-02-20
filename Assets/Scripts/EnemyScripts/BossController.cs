using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossFireObjects eye1;
    [SerializeField] private BossFireObjects eye2;
    [SerializeField] private BossFireObjects handLeft;
    [SerializeField] private BossFireObjects handRight;
    [SerializeField] private GameObject hands;
    [SerializeField] private BossMovement bm;
    [SerializeField] private SpawnController sc;
    public enum EnemyStates { quarterHP, halfHP, passive, awakened }
    private EnemyStates state = EnemyStates.passive;
    private int Health;
    private float timeBetweenActions = 5f;
    private float timeToAction = 0;
    private float speedMin = 10;
    private float speedMax = 31;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timeToAction += Time.deltaTime;
        if (timeToAction >= timeBetweenActions){
            timeToAction = 0;
            if (state != EnemyStates.passive){
                bossAction();
            }
        }
    }

    public void Awaken()
    {
        hands.SetActive(true);
        state = EnemyStates.awakened;
        bm.Awaken();
    }

    void bossAction()
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
    private IEnumerator ShootLasers(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            eye1.Fire();
            yield return new WaitForSeconds(0.3f);
            eye2.Fire();
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator ShootRockets(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            handLeft.Fire();
            yield return new WaitForSeconds(0.3f);
            handRight.Fire();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator ShootRandom(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            handLeft.FireRandom();
            yield return new WaitForSeconds(0.3f);
            handRight.FireRandom();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator SummonAdds(int rounds)
    {
        for (int i = 0; i < rounds; i++)
        {
            sc.SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
            sc.SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
