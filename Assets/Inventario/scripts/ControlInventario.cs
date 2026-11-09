
using UnityEngine;

public class ControlInventario : MonoBehaviour
{
    public GameObject inventario;

    public void AbrirCerrarInventario()
    {
        inventario.SetActive(!inventario.activeSelf);
    }
}
