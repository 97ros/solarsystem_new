using UnityEngine;

public class PlayerSpaceship : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;

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

    // Controllo abilitazione
    public bool controlsEnabled = true; // Controlli abilitati di default
    public bool cameraControlsEnabled = true; // Abilita/disabilita i controlli della telecamera

    // Forza di repulsione per le collisioni con i pianeti
    public float collisionRepulsionForce = 10f;

    // Variabili per la gestione della repulsione
    private Vector3 repulsionDirection;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        spaceshipRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gestisci i controlli per la telecamera separatamente
        if (cameraControlsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                cameraManager.SwitchCamera(cameraManager.thirdPersonVirtCam);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                cameraManager.SwitchCamera(cameraManager.firstPersonVirtCam);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                cameraManager.SwitchCamera(cameraManager.thirdPersonVirtCamFOV);
            }
            if (Input.GetKeyDown(KeyCode.M)) // Tasto per la visuale dall'alto
            {
                cameraManager.SwitchCamera(cameraManager.topDownVirtCam);
            }
        }

        // Gestisci i controlli dell'astronave solo se sono abilitati
        if (!controlsEnabled) return;

        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        if (!controlsEnabled) return; // Se i controlli sono disabilitati, esci

        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * horizontalSpeedMult, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.right * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
    }

    // Gestione delle collisioni con i pianeti
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            // Calcola la direzione opposta al punto di contatto per respingere la navicella
            repulsionDirection = (transform.position - other.transform.position).normalized;

            // Applica una forza di repulsione
            ApplyRepulsionForce();
        }
    }

    // Gestione delle collisioni con i pianeti quando la navicella rimane dentro il trigger
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            // Continua a applicare la repulsione mentre la navicella è nel collider
            repulsionDirection = (transform.position - other.transform.position).normalized;

            // Applica una forza di repulsione
            ApplyRepulsionForce();
        }
    }

    // Funzione che applica la forza di repulsione
    void ApplyRepulsionForce()
    {
        // Applica la forza di repulsione
        spaceshipRB.AddForce(repulsionDirection * collisionRepulsionForce, ForceMode.Impulse);
    }
}
