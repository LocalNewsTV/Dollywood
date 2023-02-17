using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
     private int maxEnemies = 5;
    GameObject[] numEnemies;
    void Start()
    {
        
    }

    void Update()
    {
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (numEnemies.Length < maxEnemies)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = spawnPoint;
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);

        }
    }
}
