using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayMenuButton : MonoBehaviour
{
    [SerializeField] private FonduNoirAnimation FadeOut;
    [SerializeField] private GameObject FO;
    [SerializeField] private AsyncOperation asyncOperation;

    private void Start()
    {
        // on lance le cahrgement de la scene suivante
        asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);  
        // on l'empeche de de se lancer
        asyncOperation.allowSceneActivation = false;
        // on recupere le script du fondu
        FadeOut = FO.GetComponent<FonduNoirAnimation>();
    }
    public void PlayGame ()
    {
        // lorsque le boutn play est activé on lance le fondu noir
        FadeOut.FadeOut = true;
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        // si le fondu a termine et a un alpha a 1 on lance la scene
        if (FadeOut.Transparence >= 1)
        {
            Debug.Log("on entre dans le if");
            asyncOperation.allowSceneActivation = true;
        }
    }

    // quitter le jeu
    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
}
