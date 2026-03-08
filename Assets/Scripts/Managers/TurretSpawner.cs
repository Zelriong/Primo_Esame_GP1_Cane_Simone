using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSpawner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject[] turretBases; //array delle varie torrette

    bool isEmpty; // per quando la pedana è ancora libera
    

    
    private void Start()
    {
        isEmpty = true;

    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (GameManager.instance.building == true && GameManager.instance.maxTurretCount >= 0) //solo se è stato premuto un bottone (building) e le pedane sono ancora libere
        {

            if (isEmpty == true && gameObject.layer == 8) //se quella pedana (layer n 8) è effettivamente libera
            {
                int i = GameManager.instance.turretNum; //prende il numero della torretta settato dal bottone


                Instantiate(turretBases[i], transform.position, transform.rotation);
                gameObject.GetComponent<Renderer>().material.color = Color.orange;
                isEmpty = false;
                GameManager.instance.building = false; //toglie la possibilità di spawnarne altre
                GameManager.instance.maxTurretCount--; //riduce il numero disponibile di piattaforme
            }
        }
        else return; //probabilmente superfluo, scaramantico

    }

   // funzioni assegnate ai vari buttons 
    public void BaseTurret()
    {
        GameManager.instance.building = true; // rende possibile costruire una torretta
        GameManager.instance.turretNum = 0; //dice quale deve essere

    }
    public void MGTurret()
    {
        GameManager.instance.building = true;
        GameManager.instance.turretNum = 1;
    }
    public void AOETurret()
    {
        
        GameManager.instance.building = true;
        GameManager.instance.turretNum = 2;
    }
}


