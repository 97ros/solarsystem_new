using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;
    public GameObject planetInfoPanel;
    public TMP_Text nameText, sizeText, distanceText, rotationTimeText, massText, temperatureText;
    public Button closeButton;
    public Animator planetPanelAnimator;

    [System.Serializable]
    public struct PlanetInfo
    {
        public string name;
        public float size;
        public float distanceFromSunValue;
        public string distanceFromSunUnit;
        public float rotationTimeValue;
        public string rotationTimeUnit;
        public float massValue;
        public int massExponent;
        public float surfaceTemperature;
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
        sizeText.text = "Dimensione (diametro): " + selectedPlanet.size + " km";
        distanceText.text = "Distanza media dal Sole: " + selectedPlanet.distanceFromSunValue + " " + selectedPlanet.distanceFromSunUnit;
        rotationTimeText.text = "Durata dell'orbita: " + selectedPlanet.rotationTimeValue + " " + selectedPlanet.rotationTimeUnit + " terrestri";
        massText.text = "Massa: " + selectedPlanet.massValue + " x 10^" + selectedPlanet.massExponent + " kg";
        temperatureText.text = "Temperatura media superficiale: " + selectedPlanet.surfaceTemperature + " °C";
    }

    // Metodo per chiudere il pannello
    void ClosePanel()
    {
        Debug.Log("ClosePanel chiamato");

        // Attiva l'animazione di chiusura
        planetPanelAnimator.SetTrigger("ClosePanel");
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