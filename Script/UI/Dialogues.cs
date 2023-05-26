using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class Dialogues : MonoBehaviour
{
    // variables
    public TextMeshProUGUI textComponent;
    [SerializeField] public string[] lines;
    [SerializeField] public float textSpeed;
    [SerializeField] public GameObject TextUI;

    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        // on récupère les texte et on appelle la fonction StartDialogue
        textComponent.text = string.Empty;
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        //si le joueur appuie sur controle
        if (Input.GetKeyDown("left ctrl"))
        {
            // on passe à la ligne suivante
            NextLine();
        }
        else
        {
            // sinon on arrete les coroutines et on garde le meme ID pour le texte
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
        
    }

    void StartDialogue()
    {
        // on initialise à 0 l'ID et on démarre la coroutine
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        // si on est encore dans la liste
        if (index < lines.Length - 1)
        {
            // on ajoute 1 à index et on passe à la ligne suivante
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        // si on est plus dans la liste (l'ID est trop grand)
        else
        {
            // on désactive le gameObject
            TextUI.SetActive(false);
        }
    }
}
