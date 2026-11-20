
using UnityEngine;

public class Pausar : MonoBehaviour
{
    public GameObject MenuPausa;

    public void AbrirCerrarMenuPausa()
    {
        MenuPausa.SetActive(!MenuPausa.activeSelf);
    }
}

