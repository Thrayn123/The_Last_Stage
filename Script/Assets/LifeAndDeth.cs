using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAndDeth : MonoBehaviour
{
    // encore des variables ... 
    
    [SerializeField] public int life = 5;
    [SerializeField] public int lifeMax = 5;
    [SerializeField] private float timeSpawn = 2.5f;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax = 2.5f;
    [SerializeField] private bool hitOrNot;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 spawnPointPosition;
    [SerializeField] private MonoBehaviour playerControlScript;
    [SerializeField] GameObject GameOverObjectUI;
    [SerializeField] private float horizontalMove;
    [SerializeField] private GameObject trap;
    [SerializeField] private Transform[] LifeSprite;
    [SerializeField] private AudioSource Audio;
    [SerializeField] private FonduNoirAnimation Fondu;
    [SerializeField] private Life lifeSprite;
    [SerializeField] private GameObject LifeCamera;
    [SerializeField] private GameObject FonduObj;
    [SerializeField] public PlayerMove PlayerMoveScript;


    //[SerializeField] private CheckPoint[] checkPoints;

    private Rigidbody2D rb;

    private void Start()
    {

        life = lifeMax;
        timer = 0;
        Time.timeScale = 1f;
        Fondu = FonduObj.GetComponent<FonduNoirAnimation>();
        PlayerMoveScript = GetComponent<PlayerMove>();
        lifeSprite = LifeCamera.GetComponent<Life>();


        GameOverObjectUI.SetActive(false);
        /*checkPoints = FindObjectsOfType(typeof(CheckPoint)) as CheckPoint[];
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint.GetCheckPoint() == 1)
            {
                //spawnPointPosition = checkPoint.gameObject.transform.position;
                spawnPointPosition = checkPoint.gameObject.transform.position;
                break;
            }
        }*/

        // on enregistre les variables pour pouvoir faire respawn (game object)

        

        spawnPoint = GameObject.Find("SpawnPoint").transform;
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;
        // on fait spawn le player
        Spawn();
        life = 5;
    }

    public void updateSpawnPosition(Vector3 newPosition)
    {
        spawnPoint.position = newPosition;
    }

    private void Update()
    {
        // timer pour prendre des dégats (pour e as se faire one shoot les 5 vies en 1 collision
        timer += Time.deltaTime;
        
        horizontalMove = Input.GetAxisRaw("Horizontal");
        
        if(timer >= timerMax)
        {
            hitOrNot = true;
        }
        else
        {
            hitOrNot = false;  
        }
    }

    public void NotDie()
    {
        // gagne une vie
        if(life < lifeMax)
        {
            life += 1;
            // change de sprite
            lifeSprite.GagneUneVie();
        }
        
        else
        {
            life = lifeMax;
        }
    }


    public void DoDie()
    {
        if (hitOrNot)
        {
            // on enlève une vie au player
            life -= 1;

            if (life <= 0)
            {
                //Game Over
                GameOverObjectUI.SetActive(true);
                Audio.Play();
                horizontalMove = 0;
                Time.timeScale = 0f;

            }
            else
            {
                lifeSprite.PerdUneVie();
                PlayerMoveScript.isPaused = true;
                Fondu.FadeOut = true;
                Invoke("Spawn", timeSpawn);
                timer = 0;

                // on fait respawn le joueur si il est mort et qu'il a encore des vies
            }
        }  
    }
  
    private void Spawn()
    {
        PlayerMoveScript.isPaused = false;
        Time.timeScale = 1f;
        Fondu.FadeOut = false;
        // on reinitialise les variables de départs (pv, position de spawn, la vitesse)
        transform.position = spawnPoint.position;
        rb.velocity = Vector2.zero;
    }
}
