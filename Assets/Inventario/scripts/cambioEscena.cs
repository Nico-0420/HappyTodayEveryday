using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioEscena : MonoBehaviour
{
    public int Nivel1;
    public void CambiarEscena()
    {
       SceneManager.LoadScene(Nivel1); 
    }
}