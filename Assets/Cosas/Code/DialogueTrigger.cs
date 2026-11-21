using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
[SerializeField] private DialogueObject dialogueObject;
[SerializeField] private Dialogue dialogue;


    void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.CompareTag("Player"))
        {
           
            dialogue.ShowDialogue(dialogueObject);
            Destroy(this.gameObject);
        }

    }
    
}
