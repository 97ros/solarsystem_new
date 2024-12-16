using UnityEngine;

public class StratRotation : MonoBehaviour
{
    public float rotationSpeed = 300f; // Velocità di rotazione regolabile
    private bool isRotating = false;  // Flag per controllare se il pianeta sta ruotando

    void Update()
    {
        // Controlla se il tasto sinistro del mouse è premuto
        if (Input.GetMouseButtonDown(0)) // 0 = Tasto sinistro del mouse
        {
            isRotating = true; // Inizia la rotazione
        }
        else if (Input.GetMouseButtonUp(0)) // Rilascia il tasto sinistro
        {
            isRotating = false; // Ferma la rotazione
        }

        // Effettua la rotazione se il tasto sinistro è premuto
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X"); // Movimento del mouse sull'asse X
            float mouseY = Input.GetAxis("Mouse Y"); // Movimento del mouse sull'asse Y

            // Ruota il pianeta in base al movimento del mouse
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
