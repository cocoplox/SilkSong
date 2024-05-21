using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel; // El panel de inventario a mostrar/ocultar

    void Start()

    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(inventoryPanel);
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false); // Aseg�rate de que el panel est� desactivado al inicio
        }
        else
        {
            Debug.LogError("Inventory Panel no est� asignado en el Inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().name != "Menu")
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); // Cambia el estado activo del panel de inventario
            Debug.Log("Inventario " + (inventoryPanel.activeSelf ? "abierto" : "cerrado"));
        }
        else
        {
            Debug.LogError("Inventory Panel no est� asignado en el Inspector.");
        }
    }
}
