using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // beaucoup trop de variables ...
    // bool    
    private bool isJumpingRequired;
    private bool isFalling;
    private bool isGrounded;
    public bool isPaused;
    private bool isWalled;
    private bool AudioPlay = false;
    private bool isWalledLeft;
    private bool isWalledRight;

    // Component Variables
    private Vector2 zeroVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float initialGravityScale;
    private Animator animator;
    private float horizontalMove;

    // variables de données (force, vitesse, ... etc)
    [SerializeField] private float speed = 6f;
    [SerializeField] private float Maxspeed = 6f;
   

    [SerializeField] private float movementSmoothing = 0.2f;
    [SerializeField] private float jumpForce = 6.5f;

    [SerializeField] private float wallJumpMultiplier = 1f;
    [SerializeField] private float velocityThreshold = 0.5f;
    [SerializeField] private float fallGravityMultiplier = 2.2f;
    [SerializeField] private float lowJumpGravityMultiplier = 2.5f;
    [SerializeField] private float wallGravityResist = 25f;
    [SerializeField] private float MaxVelocity = 5f;

    // Entre 0 et 1f --> Lerp entre up et right
    [SerializeField] private float wallJumpAngle = 0.5f;
    //Ground
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckWidth;
    [SerializeField] private float groundCheckHeight;
    //Wall
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private float wallCheckWidth;
    [SerializeField] private float wallCheckHeight;

    // box
    [SerializeField] private BoxCollider2D Run;
    // Audio
    [SerializeField] public AudioSource Audio;
    [SerializeField] public AudioSource AudioRun;
    [SerializeField] public AudioClip JumpClip;
    [SerializeField] public AudioClip RunClip;
    [SerializeField] public bool isPlaying;
    
    

    private void Awake()
    {
        // On récupère le composant Rigidbody2D du player
        rb = GetComponent<Rigidbody2D>();
        // on initialise l'animator
        animator = GetComponent<Animator>();
        // On mémorise l'echelle de gravité de départ
        initialGravityScale = rb.gravityScale;
        // On récupère le composant SpriteRenderer du player
        spriteRenderer = GetComponent<SpriteRenderer>();
        Run.enabled = true;
        isPlaying = false;
        isPaused = false;
 
    }

    void Update()
    {
        // On récupère une valeur qui vaudra -1 si on le joueur utilise la fleche gauche, 1 si il utilise la droite, et sinon 0
        horizontalMove = Input.GetAxisRaw("Horizontal");

        // Si on va à droite 
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

        // Si il y a un mouvement horizontal
        if (horizontalMove != 0)
        {
            // On dit à l'animateur qu'on est en mode course
            animator.SetBool("Running", true);
        }
        else
        {// Sinon
            // On dit à l'animateur qu'on est PAS en mode course
            animator.SetBool("Running", false);
        }

        // si le player bouge sur le sol 
        
        
        // Sinon utilise la touche de saut ET que on (touche le sol OU/ET qu'on touche le plafond)
        if (Input.GetButtonDown("Jump") && (isGrounded || isWalledLeft || isWalledRight))
        {
            // On dit qu'on veut sauter
            isJumpingRequired = true;
            animator.SetBool("Jumping", true);
            Audio.PlayOneShot(JumpClip);
        }

        //si le bool est en true
        if (isPaused)
        {
            // on met la vitesse du Player à 0
            rb.velocity = new Vector2(0, 0);
        }
            
    }

    void FixedUpdate()
    {

        // Une variable locale pour stocker la vitesse à appliquer, elle pourra être modifiée avant utilisation !
        float tempSpeed = speed;

        // On test si une zone rectangulaire au niveau des pieds ("groundCheck") se superpose à un ou plusieurs éléments du calque spécifié ("Ground")
        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundLayers) != null)
        {
            // Si on ne touchait pas le sol avant
            if (isGrounded == false)
            {
                animator.SetBool("isGrounded", true);
                // On vient de faire un Aterissage !

                animator.SetBool("Jumping", false);   
                animator.SetBool("isFalling", false);
                isFalling = false;

                // On passe la variable qui dit si on est au sol à VRAI
                isGrounded = true;
            }
        }
        else // SINON
        {
            // On touche pas le sol donc on passe la variable qui dit si on est au sol à FAUX
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        //on check si le rectangle au niveau gauche touche un mur 
        if (Physics2D.OverlapBox(wallCheckLeft.position, new Vector2(wallCheckWidth, wallCheckHeight), 0f, groundLayers) != null)
        {
            // on défini que le joueur est bien accrocher au mur (True)
            isWalledLeft = true;
            animator.SetBool("isWalled", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("Jumping", false);
            // on l'accroche au mur
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallGravityResist, float.MaxValue));
        }
        else
        {
            isWalledLeft = false;
            animator.SetBool("isWalled", false);
            
        }
        //on check si le rectangle au niveau droit touche un mur 
        if (Physics2D.OverlapBox(wallCheckRight.position, new Vector2(wallCheckWidth, wallCheckHeight), 0f, groundLayers) != null)
        {
            // on défini que le joueur est bien accrocher au mur (True)
            isWalledRight = true;
            animator.SetBool("isWalled", true);
            animator.SetBool("isFalling", true);
            animator.SetBool("Jumping", true);
            // on l'accroche au mur en le rendant immobile sur l'axe horizontal
            if (horizontalMove > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallGravityResist, float.MaxValue));
            }
        }
        else
        {
            // sinon c'est qu'il est au sol ou en l'air donc pas accrocher à un mur (False)
            isWalledRight = false;
            animator.SetBool("isWalled", false);

        }

        

        // si le player est accrocher à gauche ou a droite et qu'il est en train de descendre du mur 
        if ((isWalledLeft || isWalledRight) && isFalling)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxVelocity);
            animator.SetBool("isFalling", true);
            // on le fait glisser du mur en lui rajoutant de la gravity
            //rb.AddForce(Vector2.up * wallGravityResist);
        }
        // on acctualise sa velocite
        Vector2 targetVelocity = new Vector2(horizontalMove * tempSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);

        // si le player appuie  sur la touche de saut (True)
        if (isJumpingRequired)
        {
            //on le repasse à False
            isJumpingRequired = false;
            

            // si il est au sol
            if (isGrounded)
            {
                // on le fait sauter en lui envoyant une force vers le haut (permet le air control) en changeant sa velocité avec un nv Vector 2 pour éviter que le player fasse un bon vertical avant le air control
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
            }


            //s'il est au plafond
            

            if (isWalledLeft && !isGrounded)
            {
                animator.SetBool("isWalled", false);
                animator.SetBool("Jumping", true);
                animator.SetBool("isFalling", true);
                // s'il est sur le mur de gauche, on lui permet de sauter dans le sens inverse du mur et vers le haut
                rb.AddForce(Vector2.Lerp(Vector2.up, Vector2.right, wallJumpAngle) * jumpForce * wallJumpMultiplier, ForceMode2D.Impulse);
            }

            if (isWalledRight && !isGrounded) // on verifie que si le player est accroche à droite et est sur le sol
            {
                animator.SetBool("isWalled", true);
                animator.SetBool("Jumping", true);
                animator.SetBool("isFalling", true);

                // s'il est sur le mur de Droite, on lui permet de sauter dans le sens inverse du mur et vers le haut
                rb.AddForce(Vector2.Lerp(Vector2.up, Vector2.left, wallJumpAngle) * jumpForce * wallJumpMultiplier, ForceMode2D.Impulse);
            }

        }
        if (rb.velocity.y < -velocityThreshold)
        { // si je suis en dessous de 0.15 (velocityThreshold) je monte
            animator.SetBool("isFalling", true);
            animator.SetBool("Jumping", false);
            isFalling = true;

        }
        else
        {
            animator.SetBool("isFalling", false);
            isFalling = false;
        }

        // si il est accroché au mur
        if (rb.velocity.y < -velocityThreshold && isWalledLeft || isWalledRight)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("isWalled", true);

            isFalling = false;
        }
        else
        {
            animator.SetBool("isWalled", false);
            isFalling = false;
        }

    }


    // joue un sons en one shot
    public void PlaySoundRun()
    {
        AudioRun.PlayOneShot(RunClip);
    }

   

   

    void OnDrawGizmos()
    {
        // Dessine un cube vert à la position du groundCheck
        Gizmos.color = new Color32(0, 255, 0, 90);
        Gizmos.DrawCube(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawCube(wallCheckLeft.position, new Vector2(wallCheckWidth, wallCheckHeight));
        Gizmos.DrawCube(wallCheckRight.position, new Vector2(wallCheckWidth, wallCheckHeight));
    }
}