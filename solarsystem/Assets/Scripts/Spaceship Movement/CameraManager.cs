using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera thirdPersonVirtCam;
    public CinemachineVirtualCamera firstPersonVirtCam;
    public CinemachineVirtualCamera thirdPersonVirtCamFOV;
    public CinemachineVirtualCamera topDownVirtCam;

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCam;

    [Header("Solar Systems")]
    public GameObject mainSolarSystem;
    public GameObject miniSolarSystem;

    [Header("Player Spaceship")]
    public PlayerSpaceship playerSpaceship;

    void Start()
    {
        currentCam = startCamera;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 200;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }

        miniSolarSystem.SetActive(false);
        mainSolarSystem.SetActive(true);
    }

    void Update()
    {
        // Gestione del passaggio tra telecamere con i tasti J, K, L, M
        if (Input.GetKeyDown(KeyCode.L)) // Prima persona
        {
            SwitchCamera(firstPersonVirtCam);
        }
        if (Input.GetKeyDown(KeyCode.K)) // Terza persona FOV
        {
            SwitchCamera(thirdPersonVirtCamFOV);
        }
        if (Input.GetKeyDown(KeyCode.J)) // Terza persona
        {
            SwitchCamera(thirdPersonVirtCam);
        }
        if (Input.GetKeyDown(KeyCode.M)) // Visuale dall'alto
        {
            SwitchCamera(topDownVirtCam);
        }
    }

    public void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        currentCam = newCam;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 200;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }

        if (newCam == topDownVirtCam)
        {
            StartCoroutine(SwitchToTopDownCamera());
            playerSpaceship.SetControls(false); // Disabilita i controlli della navicella
            Cursor.lockState = CursorLockMode.None; // Sblocca il cursore
        }
        else
        {
            StartCoroutine(SwitchBackToMainSolarSystem());
            playerSpaceship.SetControls(true); // Riabilita i controlli della navicella
            Cursor.lockState = CursorLockMode.Locked; // Blocca il cursore
        }
    }

    private IEnumerator SwitchToTopDownCamera()
    {
        // Dopo 0.5 secondi, attiva il miniSolarSystem
        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(true);

        // Fai svanire mainSolarSystem dopo 2.45 secondi
        yield return new WaitForSeconds(1.5f);  // Attendere fino a 2.45 secondi
        mainSolarSystem.SetActive(false);
    }

    private IEnumerator SwitchBackToMainSolarSystem()
    {
        // Fai svanire miniSolarSystem dopo 0.5 secondi
        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(false);

        // Dopo 0.5 secondi, attiva mainSolarSystem
        yield return new WaitForSeconds(0.01f); // Ritardo prima che mainSolarSystem riappaia
        mainSolarSystem.SetActive(true);
    }
}
