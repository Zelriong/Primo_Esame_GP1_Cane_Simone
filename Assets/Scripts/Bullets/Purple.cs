using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Purple : Bullets
{

    SphereCollider coll;
    AoeTurret areaTurret;
    

    [SerializeField] Material materialAOE; //material trasparente di quando esplode


    public override void OnEnable()
    {
        base.OnEnable();
        areaTurret = GetComponentInParent<AoeTurret>();
        coll = GetComponent<SphereCollider>();


    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            StartCoroutine(OnBulletExplode());

        }

        if (collision.collider.gameObject.layer == 3) //se tocca i muri
        {
            Destroy(gameObject);
        }
    }

    IEnumerator OnBulletExplode()
    {
        transform.localScale = new Vector3(areaTurret.damageArea, areaTurret.damageArea, areaTurret.damageArea); //modifica le sue dimensioni
        gameObject.GetComponent<Renderer>().material = materialAOE; //cambia material
        coll.isTrigger = true; //diventa trapassabile
        rb.isKinematic = true; //perde l'influenza di addforce

        Collider[] colliders = Physics.OverlapSphere(transform.position, areaTurret.damageArea /2); //replica le dimensioni nuove con un overlapPhere
        {

            foreach (var collider in colliders)
            {
                collider.TryGetComponent<IEnemy>(out IEnemy enemy);


                enemy?.TakeDamage(bulletDamage); //triggera il danno una volta sola

            }

            yield return new WaitForSeconds(1f); //resta ancora in scena 1s

            Destroy(gameObject);

            yield return null;
        }

    }
}

//vecchi test se ti interessa vedere su cosa mi stavo incaponendo (incompleti e mezzi già cancellati)

    //public override void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.TryGetComponent<IEnemy>(out IEnemy enemy))
    //    {

//        //Instantiate(areaSphere, transform.position, transform.rotation);

//        StartCoroutine(WaitBeforeDestroy());



//    }

//    if ( collision.collider.gameObject.layer == 3)
//        {
//            Destroy(gameObject);

//        }

//    IEnumerator WaitBeforeDestroy()
//    {



//        Debug.Log("Danno");
//        enemy.TakeDamage(bulletDamage);

//       yield return new WaitForSeconds(1f);

//        Destroy(gameObject);
//        yield return null;
//    }
// }

