using UnityEngine;

public interface IEnemy
{
    public void Despawn();
    public void Damage(int hitDamage); //danno alla base

    public void TakeDamage(int bulletDamage); //danno subito al contatto coi proiettili

}
