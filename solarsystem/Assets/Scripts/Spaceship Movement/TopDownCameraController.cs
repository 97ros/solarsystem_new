using UnityEngine;
using Unity.Cinemachine;

public class TopDownCameraController : MonoBehaviour
{
    public float dragSpeed = 0.5f; // Aumentato per velocizzare il movimento
    private Vector3 lastMousePosition; // Posizione del mouse all'inizio del trascinamento
    private bool isDragging = false; // Stato per sapere se il mouse è premuto

    // Limiti per il movimento della telecamera
    public float margin = 50f;  // Margine di movimento per la telecamera
    public float centerY = 23570f;  // Y centrale della scena
    public float minX = -200f;    // Limite minimo per X
    public float maxX = 200f;     // Limite massimo per X
    public float minY = 23370f;   // Limite minimo per Y
    public float maxY = 23770f;   // Limite massimo per Y
    public float minZ = 154372f;    // Limite minimo per Z
    public float maxZ = 154872f;     // Limite massimo per Z (quante unità posso allontanarmi)

    // Parametri di zoom
    public float zoomSpeed = 100f;    // Velocità di zoom ridotta per renderlo più fluido
    public float minZoom = 10f;      // Zoom minimo (distanza minima)
    public float maxZoom = 300f;     // Zoom massimo (distanza massima)
    private float targetZoom;        // Zoom target per interpolazione

    private Camera mainCamera;  // Riferimento alla componente Camera principale
    private CinemachineVirtualCamera topDownVirtCam; // Riferimento alla telecamera virtuale

    // Valore iniziale della posizione Z
    private float initialZ = 154672f;

    // Velocità del movimento per X e Y (per effetto inerzia)
    private Vector3 currentVelocity = Vector3.zero;
    private float inertiaDamping = 0.1f;  // Quanto velocemente la velocità diminuisce per imitare l'inerzia

    void Start()
    {
        // Ottieni il riferimento alla Camera principale della scena
        mainCamera = Camera.main; // Camera.main è la Camera che effettivamente rende la scena
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene!");
        }

        // Ottieni il riferimento alla Cinemachine Virtual Camera
        topDownVirtCam = GetComponent<CinemachineVirtualCamera>(); // Assicurati di assegnarlo alla camera giusta

        // Imposta la posizione iniziale Z della telecamera
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, initialZ);
            targetZoom = mainCamera.transform.position.z;  // Impostiamo lo zoom iniziale come target
        }
    }

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
        // Calcola il fattore di scala per la velocità di trascinamento in base alla posizione Z
        float dragScale = Mathf.Lerp(1f, 0.1f, (transform.position.z - minZ) / (maxZ - minZ));

        // Gestione del trascinamento della telecamera per X e Y
        if (Input.GetMouseButtonDown(0)) // Inizia il trascinamento
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (isDragging)
        {
            // Calcola la differenza di movimento solo se c'è un reale spostamento
            Vector3 delta = Input.mousePosition - lastMousePosition;
            if (delta.magnitude > 0.5f) // Verifica se il mouse si è effettivamente spostato
            {
                delta *= dragSpeed * dragScale;

                // Applica il movimento alla velocità corrente (per ottenere l'effetto inerzia)
                currentVelocity = Vector3.Lerp(currentVelocity, new Vector3(-delta.x, -delta.y, 0), Time.deltaTime * 10f);

                // Interpolazione per il movimento fluido lungo X e Y
                float newX = Mathf.Lerp(transform.position.x, transform.position.x + currentVelocity.x, Time.deltaTime * 6f);
                float newY = Mathf.Lerp(transform.position.y, transform.position.y + currentVelocity.y, Time.deltaTime * 6f);

                // Applica il movimento alla telecamera lungo gli assi X e Y
                transform.position = new Vector3(newX, newY, transform.position.z);

                // Limita la posizione della telecamera per evitare che esca dalla finestra visibile
                float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
                float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
                transform.position = new Vector3(clampedX, clampedY, transform.position.z);

                lastMousePosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0)) // Termina il trascinamento
        {
            isDragging = false;
        }

        // Gestione dello zoom tramite rotella del mouse per la posizione Z
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");  // Acquisisce la rotellina del mouse
        if (scrollInput != 0)
        {
            // Modifica la posizione Z della telecamera per fare zoom in/out
            targetZoom += scrollInput * zoomSpeed;

            // Limita la posizione Z per evitare che si allontani troppo
            targetZoom = Mathf.Clamp(targetZoom, minZ, maxZ);
        }

        // Interpolazione dello zoom per renderlo fluido
        float smoothZoom = Mathf.Lerp(transform.position.z, targetZoom, Time.deltaTime * 5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, smoothZoom);

        // Gestione dello zoom con il touchpad (PC)
        if (Input.touchCount > 0)
        {
            // Rileva il movimento delle due dita sul touchpad
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calcola la distanza tra le due dita
            float prevDistance = (touch1.position - touch2.position).magnitude - touch1.deltaPosition.magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;

            // Se la distanza cambia, zoomma
            if (Mathf.Abs(currentDistance - prevDistance) > 1f)
            {
                float zoomDelta = (currentDistance - prevDistance) * zoomSpeed * 0.01f;
                targetZoom -= zoomDelta;

                // Limita la posizione Z per evitare che si allontani troppo
                targetZoom = Mathf.Clamp(targetZoom, minZ, maxZ);
            }
        }
    }
}