using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Riferimenti agli oggetti della scena
    public GameObject CanvaTablet;
    public GameObject EventSystemTablet;
    public GameObject CameraTablet;
    public GameObject EventSystem;
    public GameObject SpaceShip;
    public Button EsplorazioneButton;

    private bool isExplorationActive = false;

    // Start è chiamato all'inizio della scena
    void Start()
    {
        // Disabilita gli oggetti iniziali
        EventSystem.SetActive(false);
        SpaceShip.SetActive(false);

        // Abilita il cursore per CanvaTablet
        SetCursorForCanvaTablet(true);

        // Associa il metodo al bottone Esplorazione
        EsplorazioneButton.onClick.AddListener(OnEsplorazioneClicked);
    }

    // Metodo che gestisce il clic sul bottone Esplorazione
    void OnEsplorazioneClicked()
    {
        // Disabilita gli oggetti legati al CanvaTablet
        CanvaTablet.SetActive(false);
        EventSystemTablet.SetActive(false);
        CameraTablet.SetActive(false);

        // Abilita gli oggetti EventSystem e SpaceShip
        EventSystem.SetActive(true);
        SpaceShip.SetActive(true);

        // Disabilita il cursore per EventSystem/SpaceShip
        SetCursorForCanvaTablet(false);

        // Cambia lo stato dell'esplorazione
        isExplorationActive = true;
    }

    // Metodo che gestisce la pressione del tasto "T"
    void Update()
    {
        // Se si preme "T" e l'esplorazione è attiva, torna alla modalità iniziale
        if (isExplorationActive && Input.GetKeyDown(KeyCode.T))
        {
            // Riattiva gli oggetti di CanvaTablet
            CanvaTablet.SetActive(true);
            EventSystemTablet.SetActive(true);
            CameraTablet.SetActive(true);

            // Disabilita gli oggetti EventSystem e SpaceShip
            EventSystem.SetActive(false);
            SpaceShip.SetActive(false);

            // Riabilita il cursore per CanvaTablet
            SetCursorForCanvaTablet(true);

            // Cambia lo stato dell'esplorazione
            isExplorationActive = false;
        }
    }

    // Funzione per abilitare o disabilitare il cursore sul CanvaTablet
    private void SetCursorForCanvaTablet(bool isEnabled)
    {
        if (isEnabled)
        {
            // Abilita il cursore del mouse su CanvaTablet
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Aggiungi EventSystem per il mouse sul CanvaTablet
            EventSystem.SetActive(false); // Disattiva EventSystem principale
        }
        else
        {
            // Ripristina il cursore a come era prima
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Rimuovi EventSystem per il CanvaTablet
            EventSystem.SetActive(true); // Riattiva EventSystem principale
        }
    }
}
