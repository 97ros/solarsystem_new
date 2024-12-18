using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneManagerController : MonoBehaviour
{
    // Riferimenti agli oggetti
    public GameObject CanvaTablet; // Canvas italiano
    public GameObject CanvaTabletInglese; // Canvas inglese
    public GameObject EventSystemTablet;
    public GameObject EventSystem;
    public GameObject SpaceShip;
    public Button EsplorazioneButton;

    // Riferimento a TopDownCameraController
    public TopDownCameraController topDownCameraController;

    // Riferimento a CameraManager
    public CameraManager cameraManager;

    // Riferimenti ai Graphic Raycaster dei due Canvas
    public GraphicRaycaster raycasterCanvasTablet;
    public GraphicRaycaster raycasterCanvasTabletInglese;

    // Variabile per tenere traccia se il cursore deve essere attivo o meno
    private bool isCursorEnabled = false;

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

        // Imposta la lingua iniziale
        LanguageManager.SetLanguage(LanguageManager.Language.Italian); // O leggi dalle preferenze del giocatore

        // Abilitiamo il Canvas corretto in base alla lingua e disabilitiamo EventSystemTablet
        UpdateTabletLanguage();
        EventSystemTablet.SetActive(true);

        // Abilitiamo il cursore del mouse per l'oggetto CanvaTablet
        EnableMouseCursor(true);

        // Disabilitiamo la tastiera
        DisableKeyboard();
    }

    // Quando viene cliccato il bottone "Esplorazione"
    public void OnEsplorazioneButtonClicked()
    {
        // Disattiva il tablet attivo in base alla lingua e il suo Graphic Raycaster
        if (LanguageManager.CurrentLanguage == LanguageManager.Language.Italian)
        {
            CanvaTablet.SetActive(false);
            raycasterCanvasTablet.enabled = false; // Disattiva il Graphic Raycaster
        }
        else
        {
            CanvaTabletInglese.SetActive(false);
            raycasterCanvasTabletInglese.enabled = false; // Disattiva il Graphic Raycaster
        }
        EventSystemTablet.SetActive(false);

        EventSystem.SetActive(true);

        if (cameraManager != null)
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
            // Controlla se uno dei due tablet è già attivo
            if (CanvaTablet.activeInHierarchy || CanvaTabletInglese.activeInHierarchy)
            {
                // Se un tablet è già attivo, non fare nulla
                return;
            }

            // Controlla se la telecamera top-down è attiva
            if (cameraManager != null && CameraManager.IsTopDownCamActive)
            {
                // NON APRIRE IL TABLET, GESTISCI SOLO il cursore
                isCursorEnabled = true;
                EnableMouseCursor(isCursorEnabled);
            }
            else
            {
                // Siamo in modalità normale, apri il tablet come al solito

                // Riattiviamo il Canvas corretto in base alla lingua e EventSystemTablet
                UpdateTabletLanguage();
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

    // Metodo per attivare il Canvas corretto in base alla lingua
    public void UpdateTabletLanguage()
    {
        if (LanguageManager.CurrentLanguage == LanguageManager.Language.Italian)
        {
            CanvaTablet.SetActive(true);
            raycasterCanvasTablet.enabled = true; // Attiva il Graphic Raycaster
            CanvaTabletInglese.SetActive(false);
            raycasterCanvasTabletInglese.enabled = false; // Disattiva il Graphic Raycaster
            // Assicurati che la UI iniziale italiana sia attiva
            CanvaTablet.transform.Find("UIiniziale").gameObject.SetActive(true);
            // Disattiva altre UI italiane se necessario
            CanvaTablet.transform.Find("UIimpostazioni").gameObject.SetActive(false);

        }
        else // if (LanguageManager.CurrentLanguage == LanguageManager.Language.English)
        {
            CanvaTablet.SetActive(false);
            raycasterCanvasTablet.enabled = false; // Disattiva il Graphic Raycaster
            CanvaTabletInglese.SetActive(true);
            raycasterCanvasTabletInglese.enabled = true; // Attiva il Graphic Raycaster
            // Assicurati che la UI iniziale inglese sia attiva
            CanvaTabletInglese.transform.Find("UIinizialeING").gameObject.SetActive(true);
            // Disattiva altre UI inglesi se necessario
            CanvaTabletInglese.transform.Find("UIimpostazioniING").gameObject.SetActive(false);
        }
    }
}