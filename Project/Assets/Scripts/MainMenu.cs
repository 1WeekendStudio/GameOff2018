using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string newGameScene;

    public void OnStartNewGame()
    {
        SceneManager.LoadSceneAsync(this.newGameScene, LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
