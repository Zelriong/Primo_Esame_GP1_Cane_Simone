using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    [SerializeField] float spawnRate;
    [SerializeField] float shortenSpawn = 1f; //valore di accorciamento spawnrate
    float timer = 3; //così non passano 10 secondi prima di spawnare il primo nemico quando inizi un nuovo game
    int enemySpawned = 0; //counter dei nemici spawnati a ogni giro di boa


    public static event Action EnemyBuff;
    private void Update()
    {

        timer += Time.deltaTime;
        if (spawnRate <= 1) spawnRate = 1; //cap di spawn a 1 secondo
                                           //(l'avevo fatto prima di recuperare la lezione e quindi non l'ho fatto come visto in classe ma mi piaceva così),
                                           //e almeno facendo così i nemici continuano a buffarsi all'infinito

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
            spawnRate -= shortenSpawn;
            EnemyBuff?.Invoke(); //si collega al GameManager per aumentare i danni dei nemici
            enemySpawned = 0;
            return;
        }

    }


}
