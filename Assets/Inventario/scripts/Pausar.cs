using UnityEngine;

public class Pausar : MonoBehaviour
{
    public GameObject MenuPausa;
    private bool estaPausado = false;

    void Start()
    {
       
        MenuPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AbrirCerrarMenuPausa()
    {
        estaPausado = !estaPausado;
        MenuPausa.SetActive(estaPausado);


        Time.timeScale = estaPausado ? 0f : 1f;
    }
}