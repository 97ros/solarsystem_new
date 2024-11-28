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
    public float minZ = 154372f;  // Limite minimo per Z
    public float maxZ = 154872f;  // Limite massimo per Z

    // Parametri di zoom
    public float zoomSpeed = 100f;    // Velocità di zoom
    public float minZoom = 10f;      // Zoom minimo (distanza minima)
    public float maxZoom = 300f;     // Zoom massimo (distanza massima)
    private float targetZoom;        // Zoom target per interpolazione

    private Camera mainCamera;  // Riferimento alla componente Camera principale
    private CinemachineVirtualCamera topDownVirtCam; // Riferimento alla telecamera virtuale

    // Valore iniziale della posizione Z
    private float initialZ = 154672f;

    // Velocità del movimento per X e Y (per effetto inerzia)
    private Vector3 currentVelocity = Vector3.zero;
    private float inertiaDamping = 0.05f; // Ridotto per aumentare l'inerzia

    void Start()
    {
        mainCamera = Camera.main; // Camera principale
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene!");
        }

        topDownVirtCam = GetComponent<CinemachineVirtualCamera>(); // Assegna la Cinemachine Virtual Camera

        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, initialZ);
            targetZoom = mainCamera.transform.position.z;
        }
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float dragScale = Mathf.Lerp(1f, 0.1f, (transform.position.z - minZ) / (maxZ - minZ));

        // Gestione trascinamento
        if (Input.GetMouseButtonDown(0))
        {
            // Se il mouse viene premuto mentre la telecamera è in movimento (inerzia attiva), mantieni l'inerzia
            if (!isDragging && currentVelocity.magnitude > 0.01f)
            {
                isDragging = false; // Non bloccare il movimento
            }

            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            if (delta.magnitude > 0.5f)
            {
                delta *= dragSpeed * dragScale;

                // Ridurre l'effetto di damping per aumentare l'inerzia
                currentVelocity = Vector3.Lerp(currentVelocity, new Vector3(-delta.x, -delta.y, 0), Time.deltaTime * 6f);

                float newX = Mathf.Lerp(transform.position.x, transform.position.x + currentVelocity.x, Time.deltaTime * 6f);
                float newY = Mathf.Lerp(transform.position.y, transform.position.y + currentVelocity.y, Time.deltaTime * 6f);

                transform.position = new Vector3(newX, newY, transform.position.z);

                float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
                float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
                transform.position = new Vector3(clampedX, clampedY, transform.position.z);

                lastMousePosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Inerzia dopo il rilascio del mouse
        if (!isDragging)
        {
            // Ridotto il damping per rallentare più lentamente la velocità
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, Time.deltaTime * 2.5f);

            transform.position += currentVelocity * Time.deltaTime;

            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);

            if (currentVelocity.magnitude < 0.01f)
            {
                currentVelocity = Vector3.zero;
            }
        }

        // Zoom con rotella del mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            targetZoom += scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZ, maxZ);
        }

        float smoothZoom = Mathf.Lerp(transform.position.z, targetZoom, Time.deltaTime * 5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, smoothZoom);

        // Zoom con touchpad
        if (Input.touchCount > 1)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float prevDistance = (touch1.position - touch2.position).magnitude - touch1.deltaPosition.magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;

            if (Mathf.Abs(currentDistance - prevDistance) > 1f)
            {
                float zoomDelta = (currentDistance - prevDistance) * zoomSpeed * 0.01f;
                targetZoom -= zoomDelta;
                targetZoom = Mathf.Clamp(targetZoom, minZ, maxZ);
            }
        }
    }
}