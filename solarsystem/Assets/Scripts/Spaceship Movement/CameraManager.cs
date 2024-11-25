using UnityEngine;
using Unity.Cinemachine;
using System.Collections;  // Importa il namespace per le coroutine

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera thirdPersonVirtCam;
    public CinemachineVirtualCamera firstPersonVirtCam;
    public CinemachineVirtualCamera thirdPersonVirtCamFOV;
    public CinemachineVirtualCamera topDownVirtCam; // Nuova telecamera

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCam;

    [Header("Solar Systems")]
    public GameObject mainSolarSystem; // Sistema solare principale
    public GameObject miniSolarSystem; // Sistema solare in miniatura

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

        // Assicuriamoci che il sistema in miniatura sia inizialmente nascosto
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

        // Gestiamo la visibilità dei sistemi solari con il nuovo comportamento
        if (newCam == topDownVirtCam)
        {
            // Passaggio alla top-down: il main scompare dopo 0.5 sec e mini appare dopo 1.5 sec
            StartCoroutine(SwitchToTopDownCamera());
        }
        else
        {
            // Torna alle altre camere: il mini scompare dopo 1.5 sec e main appare dopo 0.5 sec
            StartCoroutine(SwitchBackToMainSolarSystem());
        }
    }

    // Coroutine per gestire il passaggio alla top-down camera
    private IEnumerator SwitchToTopDownCamera()
    {
        // Nascondi il main solar system dopo 0.5 secondi
        yield return new WaitForSeconds(3.5f);
        mainSolarSystem.SetActive(false);

        // Mostra il mini solar system dopo 1.5 secondi
        yield return new WaitForSeconds(0.20f); // Totale 1.5 secondi per il mini solar system
        miniSolarSystem.SetActive(true);
    }

    // Coroutine per gestire il ritorno dalla top-down camera alle altre telecamere
    private IEnumerator SwitchBackToMainSolarSystem()
    {
        // Nascondi il mini solar system dopo 1.5 secondi
        yield return new WaitForSeconds(1f);
        miniSolarSystem.SetActive(false);

        // Mostra il main solar system dopo 0.5 secondi
        yield return new WaitForSeconds(0.05f);
        mainSolarSystem.SetActive(true);
    }
}
