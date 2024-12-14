using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;
    public GameObject planetInfoPanel;
    public TMP_Text nameText, sizeText, distanceText, rotationTimeText, massText, temperatureText;
    public Button closeButton;

    [System.Serializable]
    public struct PlanetInfo
    {
        public string name;
        public float size;
        public float distanceFromSunValue; // Valore numerico della distanza dal sole
        public string distanceFromSunUnit; // Unità di misura della distanza (milioni o miliardi di km)
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
        // Mostra il pannello delle informazioni
        planetInfoPanel.SetActive(true);

        // Aggiorna le informazioni del pianeta selezionato
        PlanetInfo selectedPlanet = planets[index - 1];
        nameText.text = selectedPlanet.name;
        sizeText.text = "Dimensione (diametro): " + selectedPlanet.size + " km";
        distanceText.text = "Distanza media dal Sole: " + selectedPlanet.distanceFromSunValue + " " + selectedPlanet.distanceFromSunUnit + " di km";
        rotationTimeText.text = "Durata dell'orbita: " + selectedPlanet.rotationTimeValue + " " + selectedPlanet.rotationTimeUnit + " terrestri";
        massText.text = "Massa: " + selectedPlanet.massValue + " x 10^" + selectedPlanet.massExponent + " kg";
        temperatureText.text = "Temperatura media superficiale: " + selectedPlanet.surfaceTemperature + " °C";
    }

    // Metodo per chiudere il pannello
    void ClosePanel()
    {
        planetInfoPanel.SetActive(false);  // Nascondi il pannello

        // Reimposta il dropdown su "Select a planet"
        planetDropdown.value = 0;
        planetDropdown.RefreshShownValue();
    }
}