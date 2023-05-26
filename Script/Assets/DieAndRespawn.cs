using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    [SerializeField] private bool autorespawn;
    [SerializeField] private float respawnTime = 5f;
    [SerializeField] private Vector2 spawnPosition;
    
    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // on initialise les variables pour le respawn des entitées (comme les caisses de soins ou autres)
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
    }

    public void DoDie()
    {
        // si l'autorespawn est en True, on fait respawn l'entitée
        if (autorespawn)
        {
            Invoke("respawn",respawnTime);
            gameObject.SetActive(false);
        }
        else
        {
            // sinon on le détruit
            Destroy(gameObject);
        }
    }


    
    private void respawn()
    {
        gameObject.SetActive(true);

        // on fait respawn l'entitée avec ses variables de bases
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
        
    }
}
