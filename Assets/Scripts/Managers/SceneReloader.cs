using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    //le tre funzioni attribuite a tutti i vari button dei menu per gestire le scene
    
    public void RestartGame()
    {
        SceneManager.LoadScene(1);

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public  void QuitGame()
    {
        Application.Quit();
    }
}
