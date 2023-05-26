using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private float ID;
    [SerializeField] public int PlayerLifeReload;
    [SerializeField] public int PlayerLifeReload1;
    private Animator animator;

    private Collider2D col;
    [SerializeField] private LifeAndDeth LifePlayer;

    private void Start()
    {
       
        col = GetComponent<Collider2D>();
        LifePlayer = FindObjectOfType<LifeAndDeth>();
        PlayerLifeReload = LifePlayer.life;
        animator = GetComponent<Animator>();
        On();

    }

    private void FixedUpdate()
    {
        PlayerLifeReload1 = LifePlayer.life;
        if (PlayerLifeReload1 != PlayerLifeReload)
        {
            PlayerLifeReload = PlayerLifeReload1;
            On();
        }
        
        
        if (active)
        {
            col.isTrigger = false;
            animator.SetBool("Enable", true);
        }
        else
        {
            col.isTrigger = true;
            animator.SetBool("Enable", false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (active)
        {
            collision.gameObject.SendMessage("DoDie", SendMessageOptions.DontRequireReceiver);
        }
    }
    public void off(float id)
    {
        if (id == ID)
        {
            active = false;
            animator.SetBool("Enable", false);
        }
    }
    public void On()
    {
        
        col.isTrigger = false;
        active = true;
        animator.SetBool("Enable", true);
    }
}
