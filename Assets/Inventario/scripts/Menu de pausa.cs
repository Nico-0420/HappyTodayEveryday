
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menudepausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pausebutton;
    public GameObject ManuPausa;
    public GameObject inventarioButton;

    public void PauseGame()
    {
        Time.timeScale = 0;

        pausebutton.SetActive(false);
        inventarioButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        ManuPausa.SetActive(false);

        pausebutton.SetActive(true);
        inventarioButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Opciones()
    {
        ManuPausa.SetActive(true);
    }
}
