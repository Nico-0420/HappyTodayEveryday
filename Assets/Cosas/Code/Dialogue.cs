using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
   [SerializeField] private TMP_Text label;

   [SerializeField] private GameObject dialogueBox;
   

   public bool IsOpen {get; private set;}

   private Typewriter twEffect;

    private void Start()
    {
      twEffect = GetComponent<Typewriter>();
      CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return twEffect.Run(dialogue, label);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        label.text = string.Empty;
        
    }
}
