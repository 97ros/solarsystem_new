using UnityEngine;

public class EscapeHandler : MonoBehaviour
{
    public GameObject canvaTablet;
    public GameObject eventSystemTablet;
    public GameObject cameraTablet;
    public GameObject eventSystem;
    public GameObject spaceShip;
    public Camera mainCamera;

    private CursorLockMode cursorLockMode;
    private bool cursorVisible;

    private void Update()
    {
        // Controlliamo se il tasto Esc è premuto
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Riattiviamo gli oggetti relativi al tablet
            canvaTablet.SetActive(true);
            eventSystemTablet.SetActive(true);
            cameraTablet.SetActive(true);

            // Disattiviamo gli oggetti per l'esplorazione
            eventSystem.SetActive(false);
            spaceShip.SetActive(false);

            // Rende il cursore visibile e lo sblocca
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
