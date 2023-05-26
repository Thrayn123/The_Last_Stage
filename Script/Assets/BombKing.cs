using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Jobs;

public class BombKing : MonoBehaviour
{
    //[SerializeField] private int groupId;
    [SerializeField] private float radius;
    [SerializeField] private float damages = 100f;
    [SerializeField] private float impulseForce = 100f;
    [SerializeField] private AnimationCurve fallOff;

    public void detonate ()
    {
        
         
        Collider2D[] tabTarget = Physics2D.OverlapCircleAll(transform.position, radius); // un tableau de collider qui récupère tous les colliders se trouvant dans l'overlaps

        foreach (Collider2D target in tabTarget)
        {
            var distance = Vector2.Distance(target.transform.position, transform.position);
            var ratio = 1- distance / radius;
            var multiplier = fallOff.Evaluate(ratio); //fallOff = variable (nom de la courbe)
                
            // on fait exploser la bombe
            //target.SendMessage("takeDamage", 50f);

            var direction = (target.transform.position - transform.position).normalized;
            Rigidbody2D rb = target.attachedRigidbody;
            // parce-que s'il n'y a pas de rigidbody on aura une erreur lorsqu'on lui affectera une force
            if (rb!= null)
            {
                rb.AddForceAtPosition(direction * impulseForce * multiplier, transform.position, ForceMode2D.Impulse);
            }
            
        }
        

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))

        {
            detonate();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 255, 0 , 0.15f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
