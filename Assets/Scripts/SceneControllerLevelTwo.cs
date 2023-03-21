using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerLevelTwo : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform Spawn1;
    [SerializeField] private Transform Spawn2;
    [SerializeField] private Transform Spawn3;
    [SerializeField] private GameObject paintings;
    [SerializeField] private GameObject storeroomDoor;
    [SerializeField] private GameObject elevatorDoor;
    private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int maxEnemies = 15;
    GameObject[] numEnemies;
    GameObject[] spawnPoints;

        void Awake(){
            Messenger.AddListener(GameEvent.REVEAL_PHOTOS, ShowDolly);
            Messenger.AddListener(GameEvent.ENEMY_SPAWN_A, SpawnOne);
            Messenger.AddListener(GameEvent.ENEMY_SPAWN_B, SpawnTwo);
            Messenger.AddListener(GameEvent.ENEMY_SPAWN_C, SpawnThree);
            Messenger.AddListener(GameEvent.UNLOCK_DOOR_A, unlockStoreroom);
            Messenger.AddListener(GameEvent.UNLOCK_EXIT, unlockElevator);
        }
        void OnDestroy(){
            Messenger.RemoveListener(GameEvent.REVEAL_PHOTOS, ShowDolly);
            Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_A, SpawnOne);
            Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_B, SpawnTwo);
            Messenger.RemoveListener(GameEvent.ENEMY_SPAWN_C, SpawnThree);
            Messenger.RemoveListener(GameEvent.UNLOCK_DOOR_A, unlockStoreroom);
            Messenger.RemoveListener(GameEvent.UNLOCK_EXIT, unlockElevator);
        }
        

    public GameObject GetPlayer(){ return Player; }
    void Start() { 
        spawnSet(Spawn1);
    }
    public void SpawnOne() { 
        spawnSet(Spawn1); 
    }
    public void SpawnTwo(){ 
        spawnSet(Spawn2);
    }
    public void SpawnThree(){ 
        spawnSet(Spawn3);
    }
    public void ShowDolly() {
        paintings.SetActive(true);
    }
    public void unlockElevator() {
        elevatorDoor.GetComponent<OpenDoor>().UnlockDoor();
    }
    public void unlockStoreroom() {
        storeroomDoor.GetComponent<OpenDoor>().UnlockDoor();
    }
    private void spawnSet(Transform spawnList)
    {
        foreach(Transform spawnPoint in spawnList)
        {
            enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPoint.transform.position;
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemy.GetComponent<ZombieAI>().SetPlayer(Player);
            enemy.GetComponent<ZombieAI>().ChangeState(ZombieAI.EnemyStates.shamble);
        }
    }

}
