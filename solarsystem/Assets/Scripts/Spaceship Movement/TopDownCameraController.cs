using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public float dragSpeed = 0.5f; // Velocità di spostamento del trascinamento
    private Vector3 lastMousePosition; // Posizione del mouse all'inizio del trascinamento
    private bool isDragging = false; // Stato per sapere se il mouse è premuto

    void OnEnable()
    {
        // Abilita il cursore quando la telecamera dall'alto è attiva
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        // Nasconde il cursore quando si passa ad altre telecamere
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Inizia il trascinamento
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (isDragging)
        {
            Vector3 delta = (Input.mousePosition - lastMousePosition) * dragSpeed;
            transform.Translate(new Vector3(-delta.x, -delta.y, 0), Space.World);
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // Termina il trascinamento
        {
            isDragging = false;
        }
    }
}