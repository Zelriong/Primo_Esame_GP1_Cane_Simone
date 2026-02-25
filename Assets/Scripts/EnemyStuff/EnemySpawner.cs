using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    [SerializeField] float spawnRate;
    float timer = 3;
    int enemySpawned = 0;


    public static event Action EnemyBuff;
    private void Update()
    {

        timer += Time.deltaTime;
        if (spawnRate <= 2) spawnRate = 2; //cap di spawn a 2 secondi

        if (timer >= spawnRate) //spwana randomicamente un nemico all'interno della lista in inspector
        {
            int randomEnemyRate = UnityEngine.Random.Range(0, enemy.Length);

            Instantiate(enemy[randomEnemyRate], transform.position, transform.rotation);
            timer = 0;
            enemySpawned++;
            return;
        }

        if (enemySpawned >= 10)
        {
            spawnRate--;
            EnemyBuff?.Invoke(); //si collega al GameManager per aumentare i danni dei nemici
            enemySpawned = 0;
            return;
        }

    }


}
