using UnityEngine;

public class BaseTurret : Turrets
{
     public override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.money -= GameManager.instance.baseTurretPrice; //toglie il suo prezzo
    }
    public override void Upgrade()
    {
        damage *= 2;
    }
}
