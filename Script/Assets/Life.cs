using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // variables
    // on creer une liste avec les sprites
    [SerializeField] private Sprite[] Lifes;
    private SpriteRenderer SpriteRdr;
    [SerializeField] public int ID=0;
    [SerializeField] private LifeAndDeth LifePlayer;
    [SerializeField] private float PVplayer;

    // Start is called before the first frame update
    void Start()
    {
        // on initialise le sprite renderer
        SpriteRdr = GetComponent<SpriteRenderer>();
        SpriteRdr.sprite = Lifes[ID];
        LifePlayer = FindObjectOfType<LifeAndDeth>();
        PVplayer = LifePlayer.life;
    }
    // perd une vie
    public void PerdUneVie()
    {
        ID += 1;
    }
    
    // gagne une vie
    public void GagneUneVie()
    {
        ID -= 1;
    }

    // on change l'ID du sprite
    private void Update()
    {
        SpriteRdr.sprite = Lifes[ID];
    }
}


/*
if (PVplayer != LifePlayer.life)
{
    ID += 1;
    PVplayer = LifePlayer.life;
}

if (ID >= Lifes.Length)
{
    ID = 0;
}

SpriteRdr.sprite = Lifes[ID];
*/