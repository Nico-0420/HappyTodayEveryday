
using UnityEngine;

public class ControlInventario : MonoBehaviour
{
    public GameObject Inventario;

    public void AbrirCerrarInventario()
    {
        Inventario.SetActive(!Inventario.activeSelf);
    }
}
