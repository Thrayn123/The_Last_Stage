using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformeDestroy : MonoBehaviour
{
    [SerializeField] private float timeToLive = 3f;
    [SerializeField] private float timeToLiveMax = 3f;
    [SerializeField] private BoxCollider2D myCollider;
    private Collider2D rb;
    private SpriteRenderer SR;

    private void Start()
    {
        rb = GetComponent<Collider2D>();
        rb.enabled = true;
        SR = GetComponent<SpriteRenderer>();
        timeToLive = timeToLiveMax;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        timeToLive -= Time.fixedDeltaTime;
        if (timeToLive <= 0)
        {
            rb.enabled = false;
            SR.enabled = false;
            Invoke("revive",3f);
        }
    }


    void revive()
    {
        timeToLive = timeToLiveMax;
        rb.enabled = true;
        SR.enabled = true;
    }

}
