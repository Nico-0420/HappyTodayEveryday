using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rejilla : MonoBehaviour
{
    public GameObject vistaRejilla;
    public Image imagenRejilla;
    public Sprite rejillaCerrada;
    public Sprite rejillaAbierta;

    bool abierta = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            imagenRejilla.sprite = rejillaCerrada;
            vistaRejilla.SetActive(true);
        }
    }

    public void AlHacerClic()
    {
        if (abierta == false)
        {
            imagenRejilla.sprite = rejillaAbierta;
            abierta = true;
        }
        else
        {
            SceneManager.LoadScene("Laberinto");
        }
    }
}