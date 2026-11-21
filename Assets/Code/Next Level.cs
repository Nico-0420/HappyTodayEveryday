using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Animator transitionAnim;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            next();
        }
    }
    public void next()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
         SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
         transitionAnim.SetTrigger("start");
    }
}
