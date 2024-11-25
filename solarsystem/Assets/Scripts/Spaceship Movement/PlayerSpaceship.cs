using UnityEngine;
using Unity.Cinemachine;

public class PlayerSpaceship : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;

    Rigidbody spaceshipRB;

    //Inputs
    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;

    //Speed Multipliers
    [SerializeField]
    float speedMult = 1;
    [SerializeField]
    float speedMultAngle = 0.5f;
    [SerializeField]
    float speedRollMultAngle = 0.05f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        spaceshipRB = GetComponent<Rigidbody>();
    }

    void Update()
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

        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.right * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
    }
}
