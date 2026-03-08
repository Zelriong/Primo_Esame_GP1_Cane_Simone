using UnityEngine;

public class MachineGun : Turrets
{
    [SerializeField] float fireRateAccel;

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.money -= GameManager.instance.mgTurretPrice;
    }
    public override void Upgrade()
    {
        fireRate += fireRateAccel;
    }
}
