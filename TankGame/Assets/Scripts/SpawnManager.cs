using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject powerup;
    public GameManager gameManager;
    public TextMeshProUGUI waveText;
    private float xEnemyRange = 25;
    private float zEnemyRange = 20;
    private float xPowerupRange = 15.0f;
    private float zPowerupRange = 5.0f;
    public int enemyCount;
    public int powerUpCount;
    public int waveNumber ;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void GameStart()
    {
        SpawnEnemies(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && gameManager.gameIsActive)
        {
            waveNumber++;
            SpawnEnemies(waveNumber);
            
            if (waveNumber % 3 == 0)
            {
                Instantiate(powerup, GeneratePowerSpawn(), powerup.transform.rotation);
            }
        }


    }
    private Vector3 GeneratePowerSpawn()
    {
        float spawnX = Random.Range(-xPowerupRange, xPowerupRange);
        float spawnZ = Random.Range(-zPowerupRange, zPowerupRange);
        Vector3 powerSpawn = new Vector3(spawnX, 0, spawnZ);
        return powerSpawn;
    }

    private Vector3 GenerateEnemySpawnPosition()
    { 
        float randomX = Random.Range(-xEnemyRange,xEnemyRange);
        float zVar;
        
        if (randomX > 20 || randomX < -20)
        {
            zVar = Random.Range(-zEnemyRange,zEnemyRange); 
        }
        else
        {
            float[] zPos = new float[] { 13, -13 };
            int randomZ = Random.Range(0, zPos.Length);
            zVar = zPos[randomZ];
        }


        return new Vector3(randomX, 0.7f, zVar);
    }


    void SpawnEnemies(int numberOfEnemies)
    {
        int randomNumber = Random.Range(0, enemies.Length);

        for (int i=0; i<numberOfEnemies; i++)
        {
            Instantiate(enemies[randomNumber], GenerateEnemySpawnPosition() , enemies[randomNumber].gameObject.transform.rotation);
        }
    }

    public void Game()
    {

    }
}
