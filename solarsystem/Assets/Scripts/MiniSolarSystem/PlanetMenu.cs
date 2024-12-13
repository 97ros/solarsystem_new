using TMPro;  // Importa il namespace di TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour
{
    public TMP_Dropdown planetDropdown;     // Riferimento al Dropdown
    public GameObject planetInfoPanel;      // Pannello delle informazioni
    public TMP_Text nameText, sizeText, distanceText, rotationTimeText, massText, temperatureText;
    public Button closeButton;              // Pulsante per chiudere il pannello

    [System.Serializable]
    public struct PlanetInfo
    {
        public string name;
        public float size;
        public float distanceFromSun;
        public float rotationTime;
        public float mass;
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

        // Impostiamo il valore del dropdown su 0 per la "Select a planet"
        planetDropdown.value = 0;  // Imposta il primo elemento "Select a planet" come predefinito

        // Aggiungi il listener per il cambiamento di selezione nel dropdown
        planetDropdown.onValueChanged.AddListener(OnPlanetSelected);

        // Nascondi il pannello delle informazioni inizialmente
        planetInfoPanel.SetActive(false);

        // Aggiungi il listener per il pulsante di chiusura del pannello
        closeButton.onClick.AddListener(ClosePanel);
    }

    void OnPlanetSelected(int index)
    {
        if (index == 0)
        {
            // Se l'utente ha selezionato l'opzione vuota, non fare nulla
            planetInfoPanel.SetActive(false);
            return;
        }

        // Mostra il pannello delle informazioni
        planetInfoPanel.SetActive(true);

        // Aggiorna le informazioni del pianeta selezionato
        PlanetInfo selectedPlanet = planets[index - 1];  // Poiché l'indice 0 è l'opzione vuota
        nameText.text = selectedPlanet.name;
        sizeText.text = "Dimensione (diametro): " + selectedPlanet.size + " km";
        distanceText.text = "Distanza dal Sole: " + selectedPlanet.distanceFromSun + " km";
        rotationTimeText.text = "Tempo di rotazione: " + selectedPlanet.rotationTime + " giorni";
        massText.text = "Massa: " + selectedPlanet.mass + " x 10^24 kg";
        temperatureText.text = "Temperatura media superficiale: " + selectedPlanet.surfaceTemperature + " °C";
    }

    // Metodo per chiudere il pannello
void ClosePanel()
{
    planetInfoPanel.SetActive(false);  // Nascondi il pannello
    planetDropdown.value = 0;  // Resetta il valore del dropdown al valore di default (Select a planet)
}
}
