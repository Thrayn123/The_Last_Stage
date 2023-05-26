using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        // si le player entre dans le box collider de la killzone on appelle une fonction DoDie
        col.gameObject.SendMessage("DoDie");
       
    }
}
