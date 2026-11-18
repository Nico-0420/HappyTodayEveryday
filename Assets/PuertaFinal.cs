
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.currentKeys >= 3)
            {
                Destroy(gameObject);
                Debug.Log("¡Puerta abierta!");
            }
            else
            {
                Debug.Log("Necesitas las 3 llaves para abrir la puerta.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
