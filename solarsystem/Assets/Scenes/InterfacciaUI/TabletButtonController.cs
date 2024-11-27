using UnityEngine;

public class TabletButtonController : MonoBehaviour
{
    public SceneController sceneController; // Riferimento a SceneController
    private bool isTabletActive = false; // Stato del tablet (attivo o disattivo)

    // Funzione da chiamare quando il pulsante Tablet viene cliccato
    public void OnTabletButtonClicked()
    {
        isTabletActive = !isTabletActive; // Cambia stato del tablet
        sceneController.ToggleTabletScene(isTabletActive); // Attiva o disattiva la scena del tablet
    }
}
