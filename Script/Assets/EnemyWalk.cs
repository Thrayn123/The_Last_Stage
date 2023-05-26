using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyWalk : MonoBehaviour
{
    // les variables ... comme toujours
    private GameObject Player;
    [SerializeField] private float SightRange = 5f;
    [SerializeField] private bool sameDirection;
    [SerializeField] private LayerMask TestLayers;
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float run = 5f;
    [SerializeField] private float AttackRange = 1.2f;
    [SerializeField] private bool pingPongMode;
    [SerializeField] private bool Follow;
    [SerializeField] private bool GoodSens;
    [SerializeField] private bool isOnSightCollider2D;
    [SerializeField] private float ConfianceMax = 10f;
    [SerializeField] private float Confiance;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax = 3f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float horizontalMove;
    private Animator animator;

    // Une liste contenant les checkpoints
    [SerializeField] private Transform[] listeDestinations;

    // Sens dans lequel on va passer d'un checkpoint à un autre (-1 pour aller à l'envers)
    private int sens = 1;

    // Le numéro du checkPoint que l'on vise
    private int indexDestination;

    // Distance à laquelle on considère qu'on a atteint un checkpoint
    [SerializeField] private float seuil = 0.1f;

    


    void Start()
    {
        // initialisation
        Player = GameObject.FindWithTag("Player");
        Confiance = 0;
        timer = timerMax;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame


    void Update()
    {
        Vector2 direction= Player.transform.position - transform.position;
       
        
        horizontalMove = sens;
        if (horizontalMove > 0)
        {
            // On ne fait pas de symétrie sur le sprite du player
            spriteRenderer.flipX = false;
        } // Sinon ET Si on va à gauche
        else if (horizontalMove < 0)
        {
            // On fait une symétrie sur le sprite du player
            spriteRenderer.flipX = true;
        }
        // verification de la direction du player et de la direction de l'antagoniste 

        if(horizontalMove > 0 && direction.x > 0)
        {
            sameDirection = true;
        }
        if (horizontalMove > 0 && direction.x < 0)
        {
            sameDirection = false;
        }

        if (horizontalMove < 0 && direction.x < 0)
        {
            sameDirection = true;
        }
        if (horizontalMove < 0 && direction.x > 0)
        {
            sameDirection = false;
        }
        

        //timer pour hit & pour follow
        timer += Time.deltaTime;

        if (Follow == false)
        {
            // Une variable à laquelle j'assigne le Transform de notre checkpoint de destination
            Transform destination = listeDestinations[indexDestination];
            float distance = transform.position.x - destination.position.x;

            if (Mathf.Abs(distance) < seuil)
            {
                // On est assez proche du checkpoint pour considerer qu'on est arrivé

                // On passe au checkpoint suivant en ajoutant 1 ou -1 en fonction du sens
                indexDestination += sens;

                // SI l'index de destination est superieur ou égale au nombre d'elements de la liste OU inferieur à 0
                if (indexDestination >= listeDestinations.Length || indexDestination < 0)
                {
                    // Si on est en mode pingpong
                    if (pingPongMode)
                    {
                        // On inverse le sens
                        sens = -sens;
                        // On rechange l'index de destination pour revenir dans la plage de la liste
                        indexDestination += sens;
                    }
                    else // SINON
                    {
                        // On change l'index de destination pour revenir au premier élément
                        indexDestination = 0;
                    }
                }
            }
            else
            {
                if (distance > 0)
                {
                    // Je dois aller à gauche
                    transform.Translate(Vector3.left * Time.deltaTime * Speed);
                    animator.SetBool("Move", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                }

                if (distance < 0)
                {
                    // Je dois aller à droite
                    transform.Translate(Vector3.right * Time.deltaTime * Speed);
                    animator.SetBool("Move", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                }

            }
        }
    }


   

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        
        // si l'attaqueRange est plus grade que la distance entre le player et l'ennemis
        if (distance <= AttackRange)
        {  
            if (timer >= timerMax)
            {
                // on tue le player
                Player.SendMessage("DoDie", SendMessageOptions.DontRequireReceiver);
                // timer a 0
                timer = 0f;
                // on repred la destination
                Transform destination = listeDestinations[indexDestination];
                transform.position = destination.position;
                Confiance = 0;
                Follow = false;
                animator.SetBool("Hit", false);
                animator.SetBool("Running", false);
            }
        }
        else
        {
            // on poursuit le player
            if (IsOnSight(Player, SightRange, TestLayers) && sameDirection)
            {
                
                Confiance = ConfianceMax;
                if (Confiance >= 7)
                {
                    Follow = true;
                    FollowPlayer();
                }
                
            }

            else
            {
                // si le player n'est plus a portee, on reduit la confiance et si elle est < 7 on arrete de suivre
                Confiance -= Time.deltaTime;
                if (Confiance >= 7)
                {
                    Follow = true;
                    FollowPlayer();
                    
                    animator.SetBool("Running", true);
                    
                }
                else
                {
                    Confiance= 0;
                    Follow= false;
                    animator.SetBool("Running", false);
                }
                
            }
        }
    }

    void FollowPlayer()
    {
            
            Vector3 direction;
            // on regarde dans quelle direction aller pour suivre le player 
            if (transform.position.x > Player.transform.position.x)
            {
                direction= Vector3.left;
                animator.SetBool("Running", true);
            }
            else
            {
                direction= Vector3.right;
                animator.SetBool("Running", true);
            }
            if (transform.position.x - Player.transform.position.x < 0.1 & transform.position.x - Player.transform.position.x > -0.1)
            {
                direction= Vector3.zero;
                animator.SetBool("Running", false);
                animator.SetBool("Move", false);
                animator.SetBool("Idle", true);
            }
            if ( horizontalMove < 0.1 & horizontalMove >= 0)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Running", false);
                animator.SetBool("Move", false);
            }

            transform.Translate(direction * Time.fixedDeltaTime * run);
        
        
    }

    // on regarde avec un raycast ou est le player
    bool IsOnSight(GameObject target,float range,LayerMask targetLayer)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance <= range)
        {
            Vector2 direction = target.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, targetLayer);
            if (hit.transform == target.transform)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            isOnSightCollider2D = false;
            return false;
        }
    }

    // lorsqu'il y a une collision avec le player, le player meurt
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("DoDie");
            Confiance = 0;
            Follow = false;
        }
        
    }

    public void isOnSightCollider()
    {
        isOnSightCollider2D = true;
    }
}
