using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePauseMenu : MonoBehaviour
{
    // variables
    private bool menuActive;
    [SerializeField] GameObject UIMenuPause;
    [SerializeField] private MonoBehaviour playerControlScript;
    [SerializeField] private FonduNoirAnimation FO;
    [SerializeField] private GameObject FonduGame;

    private void Start()
    {
        // on recupere le script du fondu noir
        FO = FonduGame.GetComponent<FonduNoirAnimation>();
        // on désactive les UI
        menuActive = false;
        // on met le temps dans la scene normale
        Time.timeScale = 1f;
        FO.FadeOut = false;
    }

    public void Active()
    {
        // si le menu est actif
        if (menuActive) 
        {
            // on desactive et on reinitialise le temps 
            menuActive=false;
            
            Time.timeScale = 1f;

        }
        // sinon
        else 
        {
            // on active l'UI et on met le temps a 0 (la scene est en pause)
            menuActive = true;
            
            Time.timeScale = 0f;
        }
    }
    
    private void Update()
    {
        //on rend actif ou desactif l'UI pause
        UIMenuPause.SetActive(menuActive);
        // si on apuie sur espace
        if (Input.GetKeyDown("escape"))
        {
            
            if (menuActive)
            {
                menuActive = false;
                Time.timeScale = 1f;
            }
            else
            {
                menuActive = true;
                // met le jeu en pause en réduisant la vitesse du jeu a 0
                Time.timeScale = 0f;
            }
        }
    }
}
