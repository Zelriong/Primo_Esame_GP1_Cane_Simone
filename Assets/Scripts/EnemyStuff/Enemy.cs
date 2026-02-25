using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] int maxHealth;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] int points;
    public int currentHealth;

    public static event Action <int> OnDamageDeal;
    public static event Action <int> SetScore;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        damage += GameManager.instance.enemyTotalAddedDamage;
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6) //se tocca la base finale
        {
            Damage(damage);
            Despawn();
        }



    }
    public void Movement() //movimento non influenzato da lag
    {
        rb.linearVelocity = Vector3.left * speed * Time.fixedDeltaTime;
        rb.linearVelocity.Normalize();
        
    }
    public void Despawn()
    {
        SetScore?.Invoke(points); //evento che si collega a UIManager per gestire la valuta
        Destroy(gameObject);
    }

    public void Damage(int hitDamage)
    {
        damage = hitDamage;
        OnDamageDeal?.Invoke(damage); //evento che si collega al GameManager per gestire la vita
        Debug.Log(damage);
    }

    public void TakeDamage(int bulletDamage)
    {
        currentHealth -= bulletDamage;
        if (currentHealth <= 0) Despawn();
    }
}
