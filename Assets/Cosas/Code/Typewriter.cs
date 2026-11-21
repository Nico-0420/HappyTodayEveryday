using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;


public class Typewriter : MonoBehaviour
{
    [SerializeField] private float writingSpeed =50;
    public Coroutine Run(String text, TMP_Text label)
    {
       return StartCoroutine(TypeText(text, label));
    }

    private IEnumerator TypeText(String text, TMP_Text label)
    {
        
        float t = 0;
        int charIndex = 0;

        while(charIndex < text.Length)
        {
            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length );

            label.text = text.Substring(0,charIndex);

            yield return null;
        }

        label.text = text;
    }
}
