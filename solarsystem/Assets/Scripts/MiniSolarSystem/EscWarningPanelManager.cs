using UnityEngine;
using Unity.Cinemachine;

public class EscWarningPanelManager : MonoBehaviour
{
    public GameObject escWarningPanel; // Riferimento al pannello "EscWarning"
    public CinemachineVirtualCamera topdownVirtualCamera; // Riferimento alla telecamera "topdownvirtcam"
    public Animator escWarningAnimator; // Riferimento all'Animator del pannello

    private bool panelActive = false;
    private float panelTimer = 0f;
    private readonly float panelDisplayTime = 4f; // Durata di visualizzazione del pannello (4 secondi)

    void Start()
    {
        // Assicurati che il pannello sia disattivato all'avvio
        escWarningPanel.SetActive(false);
    }

    void Update()
    {
        // Controlla se la telecamera topdownvirtcam è attiva e se è stato premuto ESC
        if (IsTopDownCameraActive() && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPanel();
        }

        // Gestisci il timer per la scomparsa del pannello
        if (panelActive)
        {
            panelTimer += Time.deltaTime;
            if (panelTimer >= panelDisplayTime)
            {
                HidePanel();
            }
        }
    }

    bool IsTopDownCameraActive()
    {
        // Controlla se la telecamera topdownvirtcam è quella attiva
        return topdownVirtualCamera != null && topdownVirtualCamera.enabled;
    }

    void ShowPanel()
    {
        escWarningPanel.SetActive(true);
        escWarningAnimator.SetTrigger("OpenPopup");
        panelActive = true;
        panelTimer = 0f;
    }

    void HidePanel()
    {
        escWarningAnimator.SetTrigger("ClosePopup");
        panelActive = false;

        // Disattiva il pannello dopo l'animazione di chiusura (0.5f è un esempio, regolalo in base alla durata della tua animazione "ClosePopup")
        Invoke("DisablePanel", 0.5f);
    }

    void DisablePanel()
    {
        escWarningPanel.SetActive(false);
    }
}