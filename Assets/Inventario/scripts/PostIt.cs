using UnityEngine;

public class PostIt : MonoBehaviour
{
    [Header("Objeto para el inventario")]
    public GameObject inventoryItemPrefab;

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            if (inventoryController.Instance != null)
            {
                bool added = inventoryController.Instance.AddItem(inventoryItemPrefab);

                if (added)
                {
                    isCollected = true;
                    Debug.Log("Post-it agregado al inventario");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("No hay espacio en el inventario");
                }
            }
        }
    }
}
