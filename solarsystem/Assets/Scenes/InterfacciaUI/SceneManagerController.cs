using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneManagerController : MonoBehaviour
{
    // Riferimenti agli oggetti
    public GameObject CanvaTablet;
    public GameObject EventSystemTablet;
    public GameObject EventSystem;
    public GameObject SpaceShip;
    public Button EsplorazioneButton;

    // Riferimento a TopDownCameraController
    public TopDownCameraController topDownCameraController;

    // Riferimento a CameraManager
    public CameraManager cameraManager;

    // Variabile per tenere traccia se il cursore deve essere attivo o meno
    private bool isCursorEnabled = false;

    // Variabile per tenere traccia se TopDownCameraController era attivo prima di premere ESC
    private bool wasTopDownCamControllerEnabled = false;

    // Start è chiamato prima del primo frame
    void Start()
    {
        // Assicuriamoci che la scena parta con le impostazioni corrette
        InitializeScene();

        // Colleghiamo il bottone per l'azione di Esplorazione
        if (EsplorazioneButton != null)
        {
            EsplorazioneButton.onClick.AddListener(OnEsplorazioneButtonClicked);
        }
    }

    // Metodo per inizializzare la scena all'avvio
    void InitializeScene()
    {
        // Disabilitiamo EventSystem e SpaceShip
        EventSystem.SetActive(false);
        SpaceShip.SetActive(false);

        // Abilitiamo CanvaTablet e disabilitiamo EventSystemTablet
        CanvaTablet.SetActive(true);
        EventSystemTablet.SetActive(true);

        // Abilitiamo il cursore del mouse per l'oggetto CanvaTablet
        EnableMouseCursor(true);

        // Disabilitiamo la tastiera
        DisableKeyboard();
    }

    // Quando viene cliccato il bottone "Esplorazione"
    public void OnEsplorazioneButtonClicked()
    {
        CanvaTablet.SetActive(false);
        EventSystemTablet.SetActive(false);

        EventSystem.SetActive(true);

        if (wasTopDownCamControllerEnabled)
        {
            topDownCameraController.SetShouldLockCursor(false);
            topDownCameraController.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            wasTopDownCamControllerEnabled = false;
        }
        else if (cameraManager != null && !CameraManager.IsTopDownCamActive) // Correzione qui
        {
            SpaceShip.SetActive(true);
            EnableKeyboard();
            if (SpaceShip.TryGetComponent<PlayerSpaceship>(out var playerSpaceship))
            {
                playerSpaceship.SetControls(true);
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Metodo per abilitare/disabilitare il cursore del mouse
    void EnableMouseCursor(bool enable)
    {
        isCursorEnabled = enable;

        if (isCursorEnabled)
        {
            Cursor.lockState = CursorLockMode.None;  // Rende il cursore visibile e non bloccato
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;  // Blocca il cursore al centro
            Cursor.visible = false; // Nasconde il cursore
        }
    }

    // Metodo per disabilitare la tastiera (impedisce input di tastiera)
    void DisableKeyboard()
    {
        Input.ResetInputAxes();  // Resetta gli input per bloccare la tastiera
    }

    // Metodo per abilitare la tastiera
    void EnableKeyboard()
    {
        // In Unity, non c'è una funzione diretta per disabilitare la tastiera.
        // Ma possiamo usare una strategia che intercetta l'input della tastiera,
        // permettendo l'uso solo quando il cursore non è abilitato.
        // O, in alternativa, non fare nulla, lasciando che Unity gestisca gli input.
    }

    // Metodo chiamato quando il tasto Esc viene premuto
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Controlla se la telecamera top-down è attiva
            if (cameraManager != null && CameraManager.IsTopDownCamActive)
            {
                // NON APRIRE IL TABLET, GESTISCI SOLO topDownCameraController
                wasTopDownCamControllerEnabled = topDownCameraController.enabled;
                topDownCameraController.enabled = false;
                isCursorEnabled = true;
                EnableMouseCursor(isCursorEnabled);
            }
            else
            {
                // Siamo in modalità normale, apri il tablet come al solito

                // Riattiviamo CanvaTablet e EventSystemTablet
                CanvaTablet.SetActive(true);
                EventSystemTablet.SetActive(true);

                // Disattiviamo EventSystem e SpaceShip
                EventSystem.SetActive(false);
                SpaceShip.SetActive(false);

                // Riattiviamo il cursore del mouse su CanvaTablet
                EnableMouseCursor(true);

                // Disabilitiamo la tastiera
                DisableKeyboard();
            }
        }
    }
}