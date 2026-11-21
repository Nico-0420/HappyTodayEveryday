
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    private bool playerNear = false;
    private NextLevel level;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.currentKeys == 1)
            {
                Destroy(gameObject);
                Debug.Log("¡Puerta abierta!");
                level.next();
            }
            else
            {
                Debug.Log("Necesitas las llaves para abrir la puerta.");
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
