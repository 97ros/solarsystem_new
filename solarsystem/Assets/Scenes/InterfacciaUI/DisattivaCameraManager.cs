using UnityEngine;

public class DisattivaCameraManager : MonoBehaviour
{
    // Riferimenti agli oggetti
    public GameObject canvaTablet;
    public GameObject cameraManager;

    void Update()
    {
        // Verifica se CanvaTablet è attivo nella scena
        if (canvaTablet.activeInHierarchy)
        {
            // Se è attivo, disattiva CameraManager
            cameraManager.SetActive(false);
        }
        else
        {
            // Altrimenti, assicurati che CameraManager sia attivo
            cameraManager.SetActive(true);
        }
    }
}
