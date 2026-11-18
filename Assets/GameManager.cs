
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Image[] playerKeys;
    public static int currentKeys;

    void Start()
    {
        currentKeys = 0;
        UpdateCurrentKeys(0);
    }

    public void UpdateCurrentKeys(int x)
    {
        currentKeys = Mathf.Clamp(currentKeys + x, 0, 3);

        for (int i = 0; i < playerKeys.Length; i++)
        {
            playerKeys[i].enabled = currentKeys > i;
        }
    }
}

