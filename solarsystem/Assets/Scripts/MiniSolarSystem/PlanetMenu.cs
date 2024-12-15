using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // Necessario per lavorare con VideoPlayer

public class PlanetMenu : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;
    public GameObject planetInfoPanel;
    public TMP_Text nameText, distanceText, rotationTimeText, descriptionText; // Aggiungi descriptionText per la descrizione
    public Button closeButton;
    public Animator planetPanelAnimator;
    public TMP_Text funFactText; // Reference to the fun fact text UI element
    public Image planetImage; // Reference to the planet image UI element (Image for the planet's image)
    public RawImage videoDisplay; // Reference to RawImage for displaying video
    public VideoPlayer planetVideoPlayer; // Reference to the VideoPlayer component
    public RenderTexture renderTexture; // Render texture to display the video

    [System.Serializable]
    public struct PlanetInfo
    {
        public string name;
        public float distanceFromSunValue;
        public string distanceFromSunUnit;
        public float rotationTimeValue;
        public string rotationTimeUnit;
        public string funFact; // Add a field for the fun fact
        public Sprite planetImage; // Add a field for the planet's image (This should be Sprite, not Image)
        public VideoClip planetVideo; // Add a field for the planet's video (This should be VideoClip, but only for Neptune)
        public string description; // New field for the description of the planet
    }

    public PlanetInfo[] planets;

    void Start()
    {
        // Aggiungi l'opzione vuota come primo elemento
        planetDropdown.ClearOptions();
        planetDropdown.options.Add(new TMP_Dropdown.OptionData("Select a planet"));

        // Aggiungi i pianeti al dropdown
        foreach (var planet in planets)
        {
            planetDropdown.options.Add(new TMP_Dropdown.OptionData(planet.name));
        }

        // Aggiungi il listener per il cambiamento di selezione nel dropdown
        planetDropdown.onValueChanged.AddListener(OnPlanetSelected);

        // Impostiamo il valore del dropdown su 0 per la "Select a planet" DOPO aver aggiunto il listener
        planetDropdown.value = 0;
        planetDropdown.RefreshShownValue(); // Aggiorna la visualizzazione del dropdown

        // Nascondi il pannello delle informazioni inizialmente
        planetInfoPanel.SetActive(false);

        // Aggiungi il listener per il pulsante di chiusura del pannello
        closeButton.onClick.AddListener(ClosePanel);
    }

    void OnPlanetSelected(int index)
    {
        Debug.Log("OnPlanetSelected chiamato con indice: " + index);
        if (index > 0)
        {
            // Chiama UpdatePlanetInfoPanel solo se l'indice è valido (non "Select a planet")
            UpdatePlanetInfoPanel(index);
        }
        else
        {
            // Se l'utente seleziona "Select a planet", nascondi il pannello
            planetInfoPanel.SetActive(false);
        }
    }

    void UpdatePlanetInfoPanel(int index)
    {
        Debug.Log("UpdatePlanetInfoPanel chiamato");

        // Mostra il pannello delle informazioni
        planetInfoPanel.SetActive(true);

        // Attiva l'animazione di apertura
        planetPanelAnimator.SetTrigger("OpenPanel");

        // Aggiorna le informazioni del pianeta selezionato
        PlanetInfo selectedPlanet = planets[index - 1];
        nameText.text = selectedPlanet.name;
        distanceText.text = "- Distanza media dal Sole: " + selectedPlanet.distanceFromSunValue + " " + selectedPlanet.distanceFromSunUnit;
        rotationTimeText.text = "- Durata dell'orbita: " + selectedPlanet.rotationTimeValue + " " + selectedPlanet.rotationTimeUnit + " terrestri";
        funFactText.text = "Lo sapevi?\n" + selectedPlanet.funFact;

        // Visualizza la descrizione del pianeta
        descriptionText.text = selectedPlanet.description; // Aggiungi la descrizione al pannello

        // Visualizza l'immagine del pianeta
        planetImage.sprite = selectedPlanet.planetImage;

        // Se il pianeta è Nettuno, mostra il video
        if (selectedPlanet.name == "Nettuno" && selectedPlanet.planetVideo != null)
        {
            // Se c'è un video per Nettuno, mostralo
            videoDisplay.gameObject.SetActive(true); // Mostra il RawImage per il video
            planetImage.gameObject.SetActive(false); // Nascondi l'Image per l'immagine del pianeta

            // Impostare correttamente il renderMode a RenderTexture
            planetVideoPlayer.renderMode = VideoRenderMode.RenderTexture; // Impostato a RenderTexture correttamente
            planetVideoPlayer.targetTexture = renderTexture; // Collega la RenderTexture per visualizzare il video
            planetVideoPlayer.clip = selectedPlanet.planetVideo; // Imposta il video di Nettuno
            planetVideoPlayer.Play(); // Avvia il video
        }
        else
        {
            // Se non è Nettuno o non c'è un video, nascondi il video e mostra solo l'immagine
            videoDisplay.gameObject.SetActive(false); // Nascondi il RawImage
            planetImage.gameObject.SetActive(true); // Mostra l'immagine
        }
    }

    // Metodo per chiudere il pannello
    void ClosePanel()
    {
        Debug.Log("ClosePanel chiamato");

        // Attiva l'animazione di chiusura
        planetPanelAnimator.SetTrigger("ClosePanel");

        // Ferma la riproduzione del video quando si chiude il pannello
        planetVideoPlayer.Stop();
    }

    // Metodo chiamato da LateUpdate quando l'animazione di chiusura è terminata
    void ClosePanelAnimationComplete()
    {
        Debug.Log("ClosePanelAnimationComplete chiamato");

        planetInfoPanel.SetActive(false);  // Nascondi il pannello

        // Disabilita temporaneamente il listener per evitare di innescare onValueChanged
        planetDropdown.onValueChanged.RemoveListener(OnPlanetSelected);

        // Reimposta il dropdown su "Select a planet"
        planetDropdown.value = 0;
        planetDropdown.RefreshShownValue();

        // Riabilita il listener
        planetDropdown.onValueChanged.AddListener(OnPlanetSelected);
    }

    void LateUpdate()
    {
        if (planetPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("PanelClose") &&
            planetPanelAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            ClosePanelAnimationComplete();
        }
    }
}
