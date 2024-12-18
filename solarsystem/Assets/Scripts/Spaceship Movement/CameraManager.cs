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
    private CinemachineVirtualCamera lastActiveCam;

    private int activeCameraIndex = -1;

    [Header("Solar Systems")]
    public GameObject mainSolarSystem;
    public GameObject miniSolarSystem;
    private Animator miniSolarSystemAnimator; // Riferimento all'Animator

    [Header("Player Spaceship")]
    public PlayerSpaceship playerSpaceship;

    // Variabile per tenere traccia se la topDownVirtCam è attiva (public static)
    public static bool IsTopDownCamActive { get; private set; } = false;

    void Start()
    {
        currentCam = startCamera;
        lastActiveCam = currentCam;
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
        miniSolarSystemAnimator = miniSolarSystem.GetComponent<Animator>(); // Ottieni il componente Animator
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
        activeCameraIndex = (activeCameraIndex + 1) % 3;

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
        miniSolarSystemAnimator.SetTrigger("OpenUI"); // Attiva l'animazione UiOpen

        yield return new WaitForSeconds(1.5f);
        mainSolarSystem.SetActive(false);
    }

    private IEnumerator SwitchBackToMainSolarSystem()
    {
        yield return new WaitForSeconds(0.5f);
        miniSolarSystemAnimator.SetTrigger("CloseUI"); // Attiva l'animazione UiClose
        miniSolarSystem.SetActive(false);

        yield return new WaitForSeconds(0.01f);
        mainSolarSystem.SetActive(true);
    }

    // Metodo per disattivare la topDownVirtCam e tornare alla telecamera precedente
    public void DeactivateTopDownCamera()
    {
        if (IsTopDownCamActive)
        {
            SwitchCamera(lastActiveCam);
        }
    }
}