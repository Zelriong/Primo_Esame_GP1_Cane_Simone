using UnityEngine;

public class Bullets : MonoBehaviour // ,  IBullets
{
    [SerializeField] float speed;
    [SerializeField] int bulletDamage;
    Rigidbody rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward *  speed, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            enemy.TakeDamage(bulletDamage);
            
        }

        if(collision.collider.gameObject.layer == 3 || collision.collider.gameObject.layer == 7) // se il proiettile tocca o un nemico
                                                                                                 // o il collider rialzato del muro, si distrugge
        {
            Destroy(gameObject);
        }
    }

    //public void Damage(int damage)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void Despawn()
    //{
    //    throw new System.NotImplementedException();
    //}


}
