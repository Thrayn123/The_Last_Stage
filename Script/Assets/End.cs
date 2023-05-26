using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject WinMenu;
    
    private void Start()
    {
        WinMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinMenu.SetActive(true); 
        Time.timeScale = 0f;
    }
}
