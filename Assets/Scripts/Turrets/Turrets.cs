using System;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public abstract class Turrets : MonoBehaviour, IPointerClickHandler
{
    public float fireRate;
    [SerializeField] int upgradeAmount; //valore iniziale di costo per il potenziamento
    public int damage;
    [SerializeField] GameObject bulletType;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] TMP_Text upgradeTXT;

    public int upgradeCost; //valore totale del potenziamento anche dopo la moltiplicazione del valore
    float timer;

    public virtual void OnEnable()
    {
        upgradeCost = upgradeAmount; // ne ho lasciati due diversi così caso mai si volesse modificare il valore di multiplier dopo ogni volta si può fare
        upgradeTXT.text = upgradeCost.ToString();

    }

    private protected void Update()
    {
        timer += Time.deltaTime * fireRate;

        if (timer >= 5) // se il timer raggiunge questo valore, spara
        {

            Instantiate(bulletType, spawnPoint.transform.position, transform.rotation, transform);
            timer = 0;
        }

    }

    public virtual void Upgrade() //upgrade base per dare poi il custom ad ognuno separatamente
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.money >= upgradeCost)
        {
            Upgrade();
            GameManager.instance.money -= upgradeCost; //toglie prima i soldi necessari per l'upgrade
            upgradeCost *= 2; //raddoppia il costo totale
            upgradeTXT.text = upgradeCost.ToString(); //aggiorna la ui
        }

    }
}
