using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerLevelThree : MonoBehaviour
{
    [SerializeField] Transform enemySpawnPoints;
    [SerializeField] GameObject[] EnemyModels;
    [SerializeField] GameObject Player;
    [SerializeField] Transform ExperimentalWeapons;
    private int numberOfSpreads = 3;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.BOSS_SPAWN_ONE, SpawnRandom);
        Messenger.AddListener(GameEvent.BOSS_SPAWN_ALL, SpawnMany);
        Messenger.AddListener(GameEvent.EXPERIMENTAL_WEAPONS, FireExperimentalWeapons);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.BOSS_SPAWN_ONE, SpawnRandom);
        Messenger.RemoveListener(GameEvent.BOSS_SPAWN_ALL, SpawnMany);
        Messenger.RemoveListener(GameEvent.EXPERIMENTAL_WEAPONS, FireExperimentalWeapons);
    }
    private void SpawnRandom(){
        SpawnOne(enemySpawnPoints.GetChild(Random.Range(0, enemySpawnPoints.childCount)));
    }

    private void FireExperimentalWeapons(){
        int pattern = Random.Range(0, numberOfSpreads);
        foreach(Transform weapon in ExperimentalWeapons) {
            weapon.gameObject.GetComponent<BulletHell>().Fire(pattern);
        }
    }
    private void SpawnOne(Transform pos){
        GameObject enemy = Instantiate(EnemyModels[Random.Range(0, EnemyModels.Length)]);
        enemy.transform.position = pos.position;
        enemy.GetComponent<ZombieAI>().SetPlayer(Player);
        enemy.GetComponent<ZombieAI>().ChangeState(ZombieAI.EnemyStates.Hunting);
    }
    private void SpawnMany(){
        foreach(Transform point in enemySpawnPoints){
            SpawnOne(point);
        }
    }

}
