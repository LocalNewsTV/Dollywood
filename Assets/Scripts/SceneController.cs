using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject Player;
    private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int maxEnemies = 4;
    GameObject[] numEnemies;
    GameObject[] spawnPoints;
    private int minSpawn = 0;
    private int maxSpawn;
    private float timeSinceSpawn = 0;
    int enemiesToKill = 100;

    public GameObject GetPlayer()
    {
        return Player;
    }

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
        maxSpawn = spawnPoints.Length -1;
        Debug.Log(maxSpawn);
    }

    private void spawn()
    {
        enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = spawnPoints[Random.Range(minSpawn, maxSpawn)].transform.position;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        enemy.GetComponent<ZombieAI>().SetPlayer(Player);

    }
    void LateUpdate()
    {
        timeSinceSpawn += Time.deltaTime;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (numEnemies.Length < maxEnemies && timeSinceSpawn > 1 && numEnemies.Length < enemiesToKill)
        {
            timeSinceSpawn = 0;
            spawn();

        }
    }
}
