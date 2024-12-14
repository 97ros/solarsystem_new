using UnityEngine;
using UnityEngine.EventSystems;  // Per lavorare con il sistema di eventi
using UnityEngine.UI;  // Per lavorare con UI

public class SceneManagerController : MonoBehaviour
{
    // Riferimenti agli oggetti
    public GameObject CanvaTablet;
    public GameObject EventSystemTablet;
    public GameObject EventSystem;
    public GameObject SpaceShip;
    public Button EsplorazioneButton;
    
    // Variabile per tenere traccia se il cursore è attivo o meno
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
        // Disabilitiamo CanvaTablet ed EventSystemTablet
        CanvaTablet.SetActive(false);
        EventSystemTablet.SetActive(false);

        // Abilitiamo EventSystem e SpaceShip
        EventSystem.SetActive(true);
        SpaceShip.SetActive(true);

        // Disabilitiamo il cursore del mouse per tornare alla scena originale
        EnableMouseCursor(false);

        // Abilitiamo la tastiera per SpaceShip
        EnableKeyboard();
    }

    // Metodo per abilitare/disabilitare il cursore del mouse
    void EnableMouseCursor(bool enable)
    {
        if (enable)
        {
            Cursor.lockState = CursorLockMode.None;  // Rende il cursore visibile
            Cursor.visible = true;
            isCursorEnabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;  // Blocca il cursore
            Cursor.visible = false;
            isCursorEnabled = false;
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
