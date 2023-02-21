using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private bool autoSpawn = false;
    [SerializeField] private int maxSpawnAtTime;
    [SerializeField] private GameObject Player;
    [SerializeField] private int enemiesToKill = 100;

    private float timeSinceLastSpawn = 0;

    private GameObject enemy;
    private GameObject[] spawnPoints;
    private GameObject[] numEnemies;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");   
    }

    // Update is called once per frame
    void LateUpdate(){
        if (autoSpawn){
            timeSinceLastSpawn += Time.deltaTime;
            numEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(numEnemies.Length <= maxSpawnAtTime && numEnemies.Length < enemiesToKill) {
                SpawnEnemy();
            }
        }
    }
    public void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position + new Vector3(Random.Range(0,4),0,Random.Range(0,4));
        enemy.transform.LookAt(Player.transform);
        enemy.GetComponent<ZombieAI>().SetPlayer(Player);
        enemy.GetComponent<ZombieAI>().SetToKill();
    }
}
