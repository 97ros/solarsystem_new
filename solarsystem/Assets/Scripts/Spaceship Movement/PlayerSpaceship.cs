using UnityEngine;
using Unity.Cinemachine;

public class PlayerSpaceship : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;
    public Transform sistemaSolare; // Assegna l'oggetto "SistemaSolare" qui

    [Header("Cinemachine")]
    public CinemachineVirtualCamera firstPersonCamera; // Telecamera prima persona
    public CinemachineVirtualCamera thirdPersonCamera; // Telecamera terza persona
    public CinemachineVirtualCamera thirdPersonFOVCamera; // Telecamera terza persona con FOV incrementato

    Rigidbody spaceshipRB;

    // Inputs
    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;

    // Speed Multipliers
    [SerializeField]
    float speedMult = 1;
    [SerializeField]
    float horizontalSpeedMult = 0.5f;
    [SerializeField]
    float speedMultAngle = 0.5f;
    [SerializeField]
    float speedRollMultAngle = 0.05f;

    // Boost
    [SerializeField]
    float boostMultiplier = 2f; // Moltiplicatore di velocità per il boost
    private bool isBoosting = false;
    private float currentSpeedMult = 1f; // Velocità attuale
    private float targetSpeedMult = 1f; // Velocità desiderata

    // Camera FOV
    [Header("Camera FOV Settings")]
    [SerializeField]
    private float defaultFirstPersonFOV = 30f; // FOV predefinito prima persona
    [SerializeField]
    private float defaultThirdPersonFOV = 20f; // FOV predefinito terza persona
    [SerializeField]
    private float defaultThirdPersonFOVBoosted = 50f; // FOV predefinito terza persona con boost
    private float currentFirstPersonFOV;
    private float currentThirdPersonFOV;
    private float currentThirdPersonFOVBoosted;

    // Incremento di FOV tramite Inspector
    [Header("Camera Boost Settings")]
    [SerializeField]
    private float boostFOVIncrement = 10f; // Incremento FOV durante il boost

    private bool isMovingForward = false; // Per verificare se la navicella sta andando in avanti

    // Controllo abilitazione
    public bool controlsEnabled = true;

    // Forza di repulsione per le collisioni con i pianeti
    public float collisionRepulsionForce = 10f;

    // Variabili per la gestione della repulsione
    private Vector3 repulsionDirection;

    void Start()
    {
        spaceshipRB = GetComponent<Rigidbody>();

        // Imposta i valori iniziali per FOV
        currentFirstPersonFOV = defaultFirstPersonFOV;
        currentThirdPersonFOV = defaultThirdPersonFOV;
        currentThirdPersonFOVBoosted = defaultThirdPersonFOVBoosted;

        SetControls(true); // Assicura che i controlli siano abilitati all'inizio
    }

    void Update()
    {
        // Controlli per la navicella
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");

        // Controlla se il tasto Shift è premuto per attivare il boost
        isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Calcola la velocità con il boost (se attivato)
        targetSpeedMult = isBoosting ? speedMult * boostMultiplier : speedMult;

        // Interpolazione graduale verso la velocità desiderata
        currentSpeedMult = Mathf.Lerp(currentSpeedMult, targetSpeedMult, Time.deltaTime * 5f); // "5f" controlla la velocità di transizione

        // Rileva se la navicella si sta muovendo in avanti
        isMovingForward = verticalMove > 0;

        // Modifica graduale di FOV per la telecamera attiva
        UpdateCameraFOV();
    }

    void FixedUpdate()
    {
        if (!controlsEnabled) return;

        // Applica i movimenti e le rotazioni alla navicella
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * currentSpeedMult, ForceMode.VelocityChange);
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * horizontalSpeedMult, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.right * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);

        // Sposta il sistema solare nella direzione opposta al movimento della navicella
        SpostaSistemaSolare();
    }

    void SpostaSistemaSolare()
    {
        Vector3 navicellaPosizioneGlobale = transform.position;

        // Sposta il sistema solare nella direzione opposta
        sistemaSolare.position -= navicellaPosizioneGlobale;

        // Riporta la navicella vicino all'origine
        transform.position = Vector3.zero;
    }

    void UpdateCameraFOV()
    {
        // Se la navicella si muove in avanti e Shift è premuto, aumenta il FOV
        if (isMovingForward && isBoosting)
        {
            // Prima persona attiva
            if (firstPersonCamera.Priority > thirdPersonCamera.Priority && firstPersonCamera.Priority > thirdPersonFOVCamera.Priority)
            {
                firstPersonCamera.m_Lens.FieldOfView = Mathf.Lerp(firstPersonCamera.m_Lens.FieldOfView, currentFirstPersonFOV + boostFOVIncrement, Time.deltaTime * 5f);
            }
            // Terza persona attiva
            else if (thirdPersonCamera.Priority > firstPersonCamera.Priority && thirdPersonCamera.Priority > thirdPersonFOVCamera.Priority)
            {
                thirdPersonCamera.m_Lens.FieldOfView = Mathf.Lerp(thirdPersonCamera.m_Lens.FieldOfView, currentThirdPersonFOV + boostFOVIncrement, Time.deltaTime * 5f);
            }
            // Terza persona con FOV incrementato attiva
            else
            {
                thirdPersonFOVCamera.m_Lens.FieldOfView = Mathf.Lerp(thirdPersonFOVCamera.m_Lens.FieldOfView, currentThirdPersonFOVBoosted + boostFOVIncrement, Time.deltaTime * 5f);
            }
        }
        else
        {
            // Se non si sta accelerando o il boost non è attivo, ritorna ai valori predefiniti
            if (firstPersonCamera.Priority > thirdPersonCamera.Priority && firstPersonCamera.Priority > thirdPersonFOVCamera.Priority)
            {
                firstPersonCamera.m_Lens.FieldOfView = Mathf.Lerp(firstPersonCamera.m_Lens.FieldOfView, defaultFirstPersonFOV, Time.deltaTime * 5f);
            }
            else if (thirdPersonCamera.Priority > firstPersonCamera.Priority && thirdPersonCamera.Priority > thirdPersonFOVCamera.Priority)
            {
                thirdPersonCamera.m_Lens.FieldOfView = Mathf.Lerp(thirdPersonCamera.m_Lens.FieldOfView, defaultThirdPersonFOV, Time.deltaTime * 5f);
            }
            else
            {
                thirdPersonFOVCamera.m_Lens.FieldOfView = Mathf.Lerp(thirdPersonFOVCamera.m_Lens.FieldOfView, defaultThirdPersonFOVBoosted, Time.deltaTime * 5f);
            }
        }
    }

    public void SetControls(bool enabled)
    {
        controlsEnabled = enabled;

        if (controlsEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            repulsionDirection = (transform.position - other.transform.position).normalized;
            ApplyRepulsionForce();
        }
    }

        void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            repulsionDirection = (transform.position - other.transform.position).normalized;
            ApplyRepulsionForce();
        }
    }

    void ApplyRepulsionForce()
    {
        spaceshipRB.AddForce(repulsionDirection * collisionRepulsionForce, ForceMode.Impulse);
    }
}

