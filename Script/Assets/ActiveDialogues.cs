using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDialogues : MonoBehaviour
{
    [SerializeField] private GameObject DialogueUI;
    
    // Start is called before the first frame update
    void Start()
    {
        // on désactive d'office les UI dialogues
        DialogueUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si l'objet qui entre dans le box collider est le player
        if (collision.CompareTag("Player"))
        {
            // on active le gameObject DialogueUI
            DialogueUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // un fois que le player sort de la box on le redesactive
        DialogueUI.SetActive(false);
    }

}
