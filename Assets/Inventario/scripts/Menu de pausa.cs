using UnityEngine;
using UnityEngine.SceneManagement;

public class Menudepausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pausebutton;


    public void PauseGame()
    {
        Time.timeScale= 0;
        pausebutton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale= 1;
        pausebutton.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale= 1;
    }

    public void QuitGame()
    {
        Debug.Log("Close Game");
        Application.Quit();
    }



}
