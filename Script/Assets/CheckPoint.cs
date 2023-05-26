using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // variables
    [SerializeField] private bool isSafeDisaling = true;
    [SerializeField] private bool PV;
    [SerializeField] private int checkPointID;
    [SerializeField] private LifeAndDeth LifePlayer;
    [SerializeField] private GameObject Player;
    private Animator animator;

    private void Start()
    {
        // initialisation
        LifePlayer = Player.GetComponent<LifeAndDeth>();
        PV = false;
        animator = GetComponent<Animator>();
        animator.SetBool("On", true);
    }

    

    public int GetCheckPoint()
    {
        // renvoie l'id du checkpoint
        return checkPointID;
    }

    // lorsqu'il y a une collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si la collision on update le respawn du player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeAndDeth>().updateSpawnPosition(transform.position);
            animator.SetBool("On",false);
        }

        // si c'est la premiere fois qu'il passe sur le Check pointle player recupere une vie (def not die)
        if (PV == false)
        {
            PV = true;
            LifePlayer.NotDie();
        }
    }
}
