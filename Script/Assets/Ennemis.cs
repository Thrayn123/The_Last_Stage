using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class Ennemis : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform[] ListeDestination; // creation d'une liste de transform (gameObject)
    [SerializeField] private bool PingPong;
    [SerializeField] private float timeOfSleeping = 5f;
    [SerializeField] private GameObject Player;
    [SerializeField] private float Range = 5f;
    [SerializeField] private float playerDamage = 5f;
    [SerializeField] private float run = 10f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField]Rigidbody2D rb;
    private float horizontalMove;
    private int sens = 1;
    private int indexDestination = 0;
    private bool Right;
    private bool Left;
    Vector2 moveDirection;
    private bool col = false;
   



    // Start is called before the first frame update
    void Start()
    {
        transform.position = ListeDestination[0].position; // on initialise la position de la plateforme
        Player = GameObject.FindWithTag("Player");
        Right = true;
        
    }

  
    // Update is called once per frame
    void Update()
    {
        if (indexDestination < ListeDestination.Length && indexDestination >= 0)
        {
            var destination = ListeDestination[indexDestination]; // on parcour la liste
            transform.position = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * speed); // on fait se déplacer l'ennemi par frames vers la destination choisie dans la liste

            if (Vector2.Distance(transform.position, destination.position) < 0.01f) // je verifie si le game object est arrivé a la position
            {
                
                indexDestination += sens;
                if (indexDestination >= ListeDestination.Length || indexDestination < 0) // je vérifie si je suis sorti du tableau
                {
                    horizontalMove = 0;
                    Invoke("Sens", 5f);
                }    
            }

            float distance = Vector2.Distance(transform.position, Player.transform.position); // on calcule la distance entre le transform de ennemis et le transform du player
            if (PlayerInSameDirection() == true)
            {
                if (distance < Range) // si distance est plus petit ou égal à range 
                {
                    // on recupere la direction du player
                    Vector2 direction = Player.transform.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, groundLayers); // on creer un raycast vers le player
                        if (hit.collider) // si le raycast touche un collider
                        {
                            // on calcule la direction et l'angle du player 
                            float angle = Mathf.Atan(direction.x);
                            rb.rotation = angle;
                            moveDirection = direction;
                            // on deplace l'ennemis à une certaine vitesse sur le player sur l'axe x
                            rb.velocity = new Vector2(moveDirection.x, 0) * run;
                            
                            
                        }
                        
                }   
            }
        }
         
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si l'ennemi touche le player, il appelle la fonction Dodie pour tuer le player
        collision.gameObject.SendMessage("DoDie", SendMessageOptions.DontRequireReceiver); 
    }

    
    // fonction pour changer le sens
    void Sens()
    {
        // si ping pong est true, on change le sens de l'ennemi
        if (PingPong)
        {
            if (Right == true)
            {
                Right = false;
            }
            else
            {
                Right=true;
            }
            sens = -sens;
            indexDestination += sens;
        }
        // sinon on le fait revenir à l'index 0
        else
        {
            indexDestination = 0;
        }


    }
    
    bool PlayerInSameDirection()
        {
            // si l'ennemi regarde à gauche
            if (Player.transform.position.x - transform.position.x < 0 && Right == false)
            {
                return true;
            }
            if (Player.transform.position.x - transform.position.x > 0 && Right == true)
            {
                return true;
            }
            
            return false;
        }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = Player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, direction.normalized * Range);
    }
}

//Vunity:2021.3.9f1
