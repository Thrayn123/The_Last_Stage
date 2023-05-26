using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private float ID;
    [SerializeField] public int PlayerLifeReload;
    [SerializeField] public int PlayerLifeReload1;
   

    private Collider2D col;
    [SerializeField] private LifeAndDeth LifePlayer;

    private void Start()
    {
       
        col = GetComponent<Collider2D>();
        LifePlayer = FindObjectOfType<LifeAndDeth>();
        PlayerLifeReload = LifePlayer.life;
        
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
            
        }
        else
        {
            col.isTrigger = true;
           
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
            
        }
    }
    public void On()
    {
        
        col.isTrigger = false;
        active = true;
        
    }
}
