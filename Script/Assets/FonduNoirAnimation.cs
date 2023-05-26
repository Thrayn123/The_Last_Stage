using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FonduNoirAnimation : MonoBehaviour
{
    // variables
    public float Transparence;
    public bool FadeOut;
    public float Step;
   
    
    private void Start()
    {
        // on initialise la transparence a 0.9 car si elle est a 1 la scene suivante se lance
        Transparence = 0.9f;
    }

    private void Update()
    {
        // on initialise la transparence
        Transparence = Mathf.Clamp(Transparence,0,1);
        if (FadeOut)
        {
            // si Fade out = true on ajoute Step à chaques frames
            Transparence += Step;

        }
        else
        {
            // si Fade out = false on enleve Step à chaques frames
            Transparence -= Step;

        }
        

        // on update la transparence du canvas
        GetComponent<CanvasGroup>().alpha = Transparence;
    }

}
