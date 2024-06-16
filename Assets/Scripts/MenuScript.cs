using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Countine()
    {
        SceneManager.LoadScene(1);
    }
    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
