using UnityEngine;

public class AbrirCofre : MonoBehaviour
{
    [Header("Requisito de Post-It")]
    [Tooltip("ID del post-it según itemdictionary (índice en la lista + 1)")]
    [SerializeField] private int postItItemID = 1;
    [SerializeField] private int postItsRequeridos = 2;
    [SerializeField] private bool consumirPostItsAlAbrir = false;

    [Header("Animación del cofre")]
    [SerializeField] private Animator animator;
    [Tooltip("Nombre del parámetro Bool en el Animator")]
    [SerializeField] private string parametroBoolAbrir = "isOpen";

    [Header("Recompensa (Llave - opcional)")]
    [SerializeField] private GameObject llavePrefab;

    private bool cofreAbierto = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cofreAbierto) return;

        if (other.CompareTag("Player"))
        {
            RevisarYabrirCofre();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (cofreAbierto) return;

        if (other.CompareTag("Player"))
        {
            RevisarYabrirCofre();
        }
    }

    private void RevisarYabrirCofre()
    {
        if (cofreAbierto || inventoryController.Instance == null) return;

        int cantidadPostIts = inventoryController.Instance.CountItemByID(postItItemID);

        if (cantidadPostIts < postItsRequeridos)
        {
            return;
        }

        if (consumirPostItsAlAbrir)
        {
            ConsumirPostIts(postItsRequeridos);
        }

        cofreAbierto = true;

        if (animator != null && !string.IsNullOrEmpty(parametroBoolAbrir))
        {
            animator.SetBool(parametroBoolAbrir, true);
        }
        else
        {
            Debug.LogWarning("Falta asignar el Animator o el nombre del parámetro Bool en CofreConPostIts.");
        }

        if (llavePrefab != null)
        {
            inventoryController.Instance.AddItem(llavePrefab);
        }

        Debug.Log("¡Cofre abierto! isOpen = true.");
    }

    private void ConsumirPostIts(int cantidadAConsumir)
    {
        if (inventoryController.Instance == null || inventoryController.Instance.inventoryPanel == null)
            return;

        int restantes = cantidadAConsumir;

        foreach (Transform slotTransform in inventoryController.Instance.inventoryPanel.transform)
        {
            if (restantes <= 0) break;

            slot slot = slotTransform.GetComponent<slot>();
            if (slot != null && slot.currentItem != null)
            {
                item slotItem = slot.currentItem.GetComponent<item>();
                if (slotItem != null && slotItem.ID == postItItemID)
                {
                    int quitados = slotItem.RemoveFromStack(restantes);
                    restantes -= quitados;

                    if (slotItem.quantity <= 0)
                    {
                        Destroy(slot.currentItem);
                        slot.currentItem = null;
                    }
                }
            }
        }
    }
}