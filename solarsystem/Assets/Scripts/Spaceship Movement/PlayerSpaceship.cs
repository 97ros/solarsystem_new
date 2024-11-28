using UnityEngine;

public class PlayerSpaceship : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;
    public Transform sistemaSolare; // Assegna l'oggetto "SistemaSolare" qui

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
    public bool controlsEnabled = true;

    // Forza di repulsione per le collisioni con i pianeti
    public float collisionRepulsionForce = 10f;

    // Variabili per la gestione della repulsione
    private Vector3 repulsionDirection;

    void Start()
    {
        spaceshipRB = GetComponent<Rigidbody>();
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
    }

    void FixedUpdate()
    {
        if (!controlsEnabled) return;

        // Applica i movimenti e le rotazioni alla navicella
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
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