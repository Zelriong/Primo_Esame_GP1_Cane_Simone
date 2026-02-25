using System;
using UnityEngine;

public abstract class Turrets : MonoBehaviour
{
    [SerializeField] float fireRate;
    [SerializeField] int upgradeAmount;
    [SerializeField] int damage;
    [SerializeField] GameObject bulletType;
    [SerializeField] GameObject spawnPoint;
    float timer;
    private protected void Update()
    {
        timer += Time.deltaTime * fireRate;

        if (timer >= 5)
        {

            Instantiate(bulletType,spawnPoint.transform.position, transform.rotation);
            timer = 0;
        }
    }
}
