using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [Header("Paneles UI")]
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject panelOpciones;

    [Header("Configuración de Escenas")]
    [SerializeField] private string nombreEscenaMenu = "MenuInicial"; 

    public static bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);
        panelOpciones.SetActive(false); 
        Time.timeScale = 0f;
        juegoPausado = true;
    }


    public void Reanudar()
    {
        panelPausa.SetActive(false);
        panelOpciones.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void AbrirOpciones()
    {
        panelPausa.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
        panelPausa.SetActive(true);
    }

    public void SalirAlMenuInicial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void ReiniciarNivel()
{
    Time.timeScale = 1f;
    juegoPausado = false;
    panelPausa.SetActive(false);
    panelOpciones.SetActive(false);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}