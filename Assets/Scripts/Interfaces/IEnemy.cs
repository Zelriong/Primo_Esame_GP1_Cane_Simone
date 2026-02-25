using UnityEngine;

public interface IEnemy
{
    public void Despawn();
    public void Damage(int hitDamage);

    public void TakeDamage(int bulletDamage);

}
