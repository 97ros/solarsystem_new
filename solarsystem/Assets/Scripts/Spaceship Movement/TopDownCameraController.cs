using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public float dragSpeed = 0.5f; // Velocità di spostamento del trascinamento
    private Vector3 lastMousePosition; // Posizione del mouse all'inizio del trascinamento
    private bool isDragging = false; // Stato per sapere se il mouse è premuto

    void Update()
    {
        // Inizia il trascinamento quando il tasto sinistro del mouse è premuto
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition; // Salva la posizione iniziale del mouse
        }

        // Quando il mouse viene trascinato, calcola il movimento
        if (isDragging)
        {
            // Calcola la differenza tra la posizione attuale e quella precedente
            Vector3 delta = (Input.mousePosition - lastMousePosition) * dragSpeed;

            // Applica la differenza di movimento al sistema solare lungo l'asse X e Y
            transform.Translate(new Vector3(-delta.x, -delta.y, 0), Space.World); // Movimento orizzontale (x) e verticale (y)

            // Aggiorna la posizione del mouse per il prossimo frame
            lastMousePosition = Input.mousePosition;
        }

        // Termina il trascinamento quando il tasto sinistro del mouse è rilasciato
        if (Input.GetMouseButtonUp(0)) // Tasto sinistro del mouse rilasciato
        {
            isDragging = false;
        }
    }
}