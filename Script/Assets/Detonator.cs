using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private int detonId;
    public BombKing[] tabombs;
    
    
    private void OnTriggerEnter2D(Collider2D Collision)
    {
    
        tabombs = FindObjectsOfType(typeof(BombKing)) as BombKing[];
        foreach (BombKing bomb in tabombs)
        {
            bomb.detonate();
        }

       
    }
}
