using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public static ChestController Instance;

    [Header("Post-it")]
    public Image[] PostIt;

    public static int currentPostIts = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentPostIts = 0;
        UpdatePostItUI();
    }

    public void AddPostIt()
    {
        currentPostIts = Mathf.Clamp(currentPostIts + 1, 0, 2);

        UpdatePostItUI();

        Debug.Log("Post-it recogidos: " + currentPostIts);
    }

    void UpdatePostItUI()
    {
        for (int i = 0; i < PostIt.Length; i++)
        {
            PostIt[i].enabled = i < currentPostIts;
        }
    }
}