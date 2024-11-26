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
            playerSpaceship.controlsEnabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            StartCoroutine(SwitchBackToMainSolarSystem());
            playerSpaceship.controlsEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private IEnumerator SwitchToTopDownCamera()
    {
        yield return new WaitForSeconds(0.5f);
        mainSolarSystem.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(true);
    }

    private IEnumerator SwitchBackToMainSolarSystem()
    {
        yield return new WaitForSeconds(0.5f);
        miniSolarSystem.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        mainSolarSystem.SetActive(true);
    }
}
