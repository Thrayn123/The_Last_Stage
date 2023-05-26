using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;

    void Start()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    
}
