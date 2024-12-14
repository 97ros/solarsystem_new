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
    private CinemachineVirtualCamera lastActiveCam; // Memorizza l'ultima telecamera attiva prima di passare a topDownVirtCam

    private int activeCameraIndex = -1; // Indice della telecamera attiva (-1 iniziale per nessuna telecamera ciclica attiva)

    [Header("Solar Systems")]
    public GameObject mainSolarSystem;
    public GameObject miniSolarSystem;

    [Header("Player Spaceship")]
    public PlayerSpaceship playerSpaceship;

    // Variabile per tenere traccia se la topDownVirtCam è attiva
    public bool IsTopDownCamActive { get; private set; } = false;

    void Start()
    {
        currentCam = startCamera;
        lastActiveCam = currentCam; // Imposta la telecamera iniziale come ultima telecamera attiva
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
        // Gestione del ciclo tra telecamere con il tasto V
        if (Input.GetKeyDown(KeyCode.V) && currentCam != topDownVirtCam)
        {
            CycleThroughCameras();
        }

        // Gestione della visuale dall'alto con il tasto M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (currentCam == topDownVirtCam)
            {
                // Se siamo già in topDownVirtCam, torna alla telecamera precedente
                SwitchCamera(lastActiveCam);
            }
            else
            {
                // Memorizza la telecamera attuale come ultima telecamera prima di passare alla top-down
                lastActiveCam = currentCam;
                SwitchCamera(topDownVirtCam);
            }
        }
    }

    private void CycleThroughCameras()
    {
        // Incrementa l'indice della telecamera attiva
        activeCameraIndex = (activeCameraIndex + 1) % 3;

        // Seleziona la telecamera in base all'indice
        switch (activeCameraIndex)
        {
            case 0:
                SwitchCamera(thirdPersonVirtCam);
                break;
            case 1:
                SwitchCamera(firstPersonVirtCam);
                break;
            case 2:
                SwitchCamera(thirdPersonVirtCamFOV);
                break;
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
        playerSpaceship.SetControls(false, false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsTopDownCamActive = true;
    }
    else
    {
        StartCoroutine(SwitchBackToMainSolarSystem());
        playerSpaceship.SetControls(true, true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsTopDownCamActive = false;

        if (playerSpaceship.gameObject != null)
        {
            playerSpaceship.gameObject.SetActive(true);
        }
    }
}


    private IEnumerator SwitchToTopDownCamera()
    {
        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        mainSolarSystem.SetActive(false);
    }

    private IEnumerator SwitchBackToMainSolarSystem()
    {
        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(false);

        yield return new WaitForSeconds(0.01f);
        mainSolarSystem.SetActive(true);
    }
}