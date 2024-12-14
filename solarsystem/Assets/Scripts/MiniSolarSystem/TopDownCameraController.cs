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

    private bool shouldLockCursor = true;

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

        if (!shouldLockCursor)
        {
            Cursor.lockState = CursorLockMode.None; // Rendi il cursore visibile
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Blocca il cursore solo se richiesto
        }
    }

    void OnDisable()
    {
        if (shouldLockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetShouldLockCursor(bool value)
    {
        shouldLockCursor = value;
    }

    void Update()
    {
        if (IsMouseOverSlider())
        {
            isDragging = false;
            return;
        }

        float dragScale = Mathf.Lerp(1f, 0.1f, (transform.position.z - minZ) / (maxZ - minZ));

        if (Input.GetMouseButtonDown(0))
        {
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

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            targetZoom += scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZ, maxZ);
        }

        float smoothZoom = Mathf.Lerp(transform.position.z, targetZoom, Time.deltaTime * 5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, smoothZoom);
    }

    private bool IsMouseOverSlider()
    {
        RectTransform rectTransform = speedSlider.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition);
    }
}
