using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llegó a la salida");
            // por aca va lo de la cinematica si no estoy mal ... creo
        }
    }
}