using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    [SerializeField] private int interruptId;
    [SerializeField] private bool Trigger= false;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    public Trap[] trap;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // si on entre dans le collider 
    void OnTriggerEnter2D(Collider2D collider)
    {
        Trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       Trigger = false;
    }

    void Update()
    {
        if (Trigger)
        {
            // si le player appuie sur E
            if (Input.GetKeyDown("e"))
            {
                source.PlayOneShot(clip);
                trap = FindObjectsOfType(typeof(Trap)) as Trap[];
                foreach (Trap good in trap)
                {
                    // on desactive le piege
                    good.off(interruptId);
                }
            }
        }
        
    }
}
