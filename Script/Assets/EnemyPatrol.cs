using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool pingPongMode;

    // Une liste contenant les checkpoints
    [SerializeField] private Transform[] listeDestinations;

    // Sens dans lequel on va passer d'un checkpoint à un autre (-1 pour aller à l'envers)
    private int sens = 1;

    // Le numéro du checkPoint que l'on vise
    private int indexDestination;

    // Distance à laquelle on considère qu'on a atteint un checkpoint
    [SerializeField] private float seuil = 0.1f;

   void Update()
    {
        // Une variable à laquelle j'assigne le Transform de notre checkpoint de destination
        Transform destination = listeDestinations[indexDestination];
        float distance = transform.position.x - destination.position.x;

        if (Mathf.Abs(distance)<seuil)
        {
            // On est assez proche du checkpoint pour considerer qu'on est arrivé

            // On passe au checkpoint suivant en ajoutant 1 ou -1 en fonction du sens
            indexDestination += sens;

            // SI l'index de destination est superieur ou égale au nombre d'elements de la liste OU inferieur à 0
            if (indexDestination >= listeDestinations.Length || indexDestination < 0)
            {
                // Si on est en mode pingpong
                if (pingPongMode)
                {
                    // On inverse le sens
                    sens = -sens;
                    // On rechange l'index de destination pour revenir dans la plage de la liste
                    indexDestination += sens;
                }
                else // SINON
                {
                    // On change l'index de destination pour revenir au premier élément
                    indexDestination = 0;
                }
            }
        }
        else
        {
            if (distance > 0)
            {
                // Je dois aller à gauche
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (distance < 0)
            {
                // Je dois aller à droite
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
    }
}
