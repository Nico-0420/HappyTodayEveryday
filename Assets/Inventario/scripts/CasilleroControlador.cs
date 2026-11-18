using UnityEngine;

public class CasilleroControlador : MonoBehaviour
{
    private Animator animator;
    private bool isOpen= false;

    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen=!isOpen;
            animator.SetBool("isOpen", isOpen);
        }
    }
}
