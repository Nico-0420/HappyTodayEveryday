using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarreLlaves : MonoBehaviour
{
    public GameManager GameManager { get; private set; }
    public Dialogue dialogue;
    public DialogueObject dialog;

    void Start()
    {

        GameManager = FindAnyObjectByType<GameManager>();
        
        if (GameManager == null)
        {
            Debug.LogWarning("No se encontró el GameManager en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Key"))
        {

            if (GameManager != null)
            {
                GameManager.UpdateCurrentKeys(1);
            }

            Destroy(collision.gameObject);
             dialogue.ShowDialogue(dialog);
            
        }
    }

    IEnumerator PickItem()
    {
        yield return null; 
    }
}