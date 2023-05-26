using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class plateformMoves : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform[] ListeDestination; // creation d'une liste de transform (gameObject)
    [SerializeField] private bool PingPong;
    private int sens=1;
    private int indexDestination;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = ListeDestination[0].position; // on initialise la position de la plateforme
    }

    // Update is called once per frame
    void Update()
    {
        var destination = ListeDestination[indexDestination]; // on parcour la liste
        transform.position = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * speed); // on fait se déplacer la plateforme par frames vers la destination choisie dans la liste
        
        if (Vector2.Distance(transform.position, destination.position )< 0.01f) // je verifie si le game object est arriver a la position
        {
            indexDestination += sens;
            if (indexDestination >= ListeDestination.Length || indexDestination<0) // je vérifie si je suis sorti du tableau
            {
                if (PingPong)
                {
                    sens = -sens;
                    indexDestination += sens;
                }
                else
                {
                    indexDestination = 0;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform, true);
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            collision.gameObject.transform.SetParent(transform, true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
        if (collision.gameObject.CompareTag("Object"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }

}

