using UnityEngine;

public class Sonido : MonoBehaviour
{
    public AudioSource music;
    

    public AudioClip musica;
    


   
    void Start()
    {
        music.clip = musica;
        music.Play();
    }

   
}
