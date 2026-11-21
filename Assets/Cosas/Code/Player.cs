using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    public Dialogue DialogueUI => dialogue; 

    public Interactable Interactable { get; set;}

    

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
        {
           Interactable?.Interact(this);
        }

    }
}
