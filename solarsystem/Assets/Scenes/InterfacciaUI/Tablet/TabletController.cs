using UnityEngine;

public class TabletController : MonoBehaviour
{
    public Camera tabletCamera; // Telecamera per il tablet
    public GameObject tabletUI; // Oggetti della UI del tablet (opzionali)

    private bool isTabletActive = false; // Stato attivazione/disattivazione tablet

    // Funzione per attivare/disattivare il tablet
    public void ToggleTablet()
    {
        isTabletActive = !isTabletActive; // Inverti lo stato del tablet

        // Attiva/disattiva la telecamera del tablet
        if (tabletCamera != null)
            tabletCamera.enabled = isTabletActive;

        // Attiva/disattiva la UI del tablet
        if (tabletUI != null)
            tabletUI.SetActive(isTabletActive);
    }
}
