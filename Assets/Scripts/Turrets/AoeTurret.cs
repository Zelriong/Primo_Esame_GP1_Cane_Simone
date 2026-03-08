using UnityEngine;

public class AoeTurret : Turrets
{
    public float damageArea = 4; //area base
    public float damageAreaUpgradeMultiplier = 1.25f; //multiplier dell'area

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.money -= GameManager.instance.aoeTurretPrice;
    }
    public override void Upgrade()
    {
        damageArea *= damageAreaUpgradeMultiplier;
    }
}
