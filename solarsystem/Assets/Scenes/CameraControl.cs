using UnityEngine;
using Unity.Cinemachine;

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Riferimento alla telecamera Top Down (può essere una Camera o un GameObject generico)
    public GameObject topDownVirtCam;

    // Variabile per tenere traccia se il tasto Esc è attivo
    private bool isEscEnabled = true;

    void Update()
{
    CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
    if (brain != null && brain.ActiveVirtualCamera == topDownVirtCam.GetComponent<CinemachineVirtualCamera>())
    {
        isEscEnabled = false;
    }
    else
    {
        isEscEnabled = true;
    }

    if (isEscEnabled && Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("Tasto Esc premuto!");
        // Logica per il menu pausa
    }
}
}