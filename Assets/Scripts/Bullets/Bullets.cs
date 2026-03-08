using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [SerializeField] float speed;
    
     public int bulletDamage;
   public Rigidbody rb;
    Turrets turret;

   
    public virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        turret = GetComponentInParent<Turrets>();
        rb.AddForce(transform.forward *  speed, ForceMode.Impulse);
        bulletDamage = turret.damage; //prende il danno assegnato alla torre, so che non servirebbe e basterebbe chiamare solo la seconda poi
                                      //"TakeDamage(turret.damag)", ma ormai mi ero affezionato, e preferisco evitare di fare casini togliendolo
    }
    
    public virtual void OnCollisionEnter(Collision collision)
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

    

  


}
