using UnityEngine;

public class TabletController : MonoBehaviour
{
    public Camera tabletCamera; // Telecamera per il tablet
    public GameObject tabletUI; // Oggetti del tablet (opzionale)

    private bool isTabletActive = false;

    public void ToggleTablet()
    {
        isTabletActive = !isTabletActive;

        // Attiva/disattiva la telecamera del tablet
        if (tabletCamera != null)
            tabletCamera.enabled = isTabletActive;

        // Attiva/disattiva eventuali UI del tablet
        if (tabletUI != null)
            tabletUI.SetActive(isTabletActive);
    }
}
