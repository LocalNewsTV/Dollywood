using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneControllerLevelOne : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform Spawn1;
    [SerializeField] private Transform Spawn2;
    [SerializeField] private Transform Spawn3;
    [SerializeField] private Transform Spawn4;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_SPAWN_A, SpawnOne);
        Messenger.AddListener(GameEvent.ENEMY_SPAWN_B, SpawnTwo);
        Messenger.AddListener(GameEvent.ENEMY_SPAWN_C, SpawnThree);
        Messenger.AddListener(GameEvent.ENEMY_SPAWN_D, SpawnFour);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_A, SpawnOne);
        Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_B, SpawnTwo);
        Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_C, SpawnThree);
        Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_D, SpawnFour);
    }

    private void SpawnOne() { spawnSet(Spawn1); }
    private void SpawnTwo() { spawnSet(Spawn2); }
    private void SpawnThree() { spawnSet(Spawn3); }
    private void SpawnFour(){ spawnSet(Spawn4); }

    private void spawnSet(Transform spawnList)
    {
        foreach (Transform spawnPoint in spawnList)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            enemy.transform.position = spawnPoint.transform.position;
            enemy.transform.Rotate(0, Random.Range(0, 360), 0);
            enemy.GetComponent<ZombieAI>().SetPlayer(Player);
            enemy.GetComponent<ZombieAI>().ChangeState(ZombieAI.EnemyStates.Shamble);
            enemy.transform.localScale = enemy.transform.localScale * 0.55f;
            enemy.GetComponent<NavMeshAgent>().stoppingDistance = 0.0f;
            enemy.GetComponent<ZombieAI>().AdjustScale(0.55f);
        }
    }
}
