using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class TopDownCameraController : MonoBehaviour
{
    public float dragSpeed = 0.5f;
    private Vector3 lastMousePosition;
    private bool isDragging = false;

    public float margin = 50f;
    public float centerY = 23570f;
    public float minX = -200f;
    public float maxX = 200f;
    public float minY = 23370f;
    public float maxY = 23770f;
    public float minZ = 154372f;
    public float maxZ = 154872f;

    public float zoomSpeed = 100f;
    public float minZoom = 10f;
    public float maxZoom = 300f;
    private float targetZoom;

    private Camera mainCamera;
    private CinemachineVirtualCamera topDownVirtCam;

    private float initialZ = 154672f;
    private Vector3 currentVelocity = Vector3.zero;
    private float inertiaDamping = 0.05f;

    // Riferimento allo slider
    public Slider speedSlider;
    
    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene!");
        }

        topDownVirtCam = GetComponent<CinemachineVirtualCamera>();

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
        // Verifica se il cursore è sopra lo slider
        if (IsMouseOverSlider())
        {
            isDragging = false;  // Disabilita il drag della telecamera se il mouse è sopra lo slider
            return;  // Esci dalla funzione Update per evitare che il movimento della telecamera continui
        }

        // Il codice per il movimento e zoom della telecamera rimane invariato
        float dragScale = Mathf.Lerp(1f, 0.1f, (transform.position.z - minZ) / (maxZ - minZ));

        // Gestione trascinamento
        if (Input.GetMouseButtonDown(0))
        {
            if (!isDragging && currentVelocity.magnitude > 0.01f)
            {
                isDragging = false;
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

    // Funzione che verifica se il cursore è sopra lo slider
    private bool IsMouseOverSlider()
    {
        RectTransform rectTransform = speedSlider.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition);
    }
}
