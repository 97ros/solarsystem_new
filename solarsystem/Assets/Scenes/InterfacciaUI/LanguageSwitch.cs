using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitch : MonoBehaviour
{
    public GameObject canvaTablet;
    public GameObject canvaTabletInglese;

    public Button buttonItaliano;
    public Button buttonInglese;

    void Start()
    {
        // Assicurati che i bottoni siano attivi
        buttonItaliano.onClick.AddListener(SwitchToItalian);
        buttonInglese.onClick.AddListener(SwitchToEnglish);

        // Imposta lo stato iniziale
        canvaTablet.SetActive(true);
        canvaTabletInglese.SetActive(false);
    }

    // Metodo pubblico senza parametri
    public void SwitchToItalian()
    {
        canvaTablet.SetActive(true);
        canvaTabletInglese.SetActive(false);
    }

    // Metodo pubblico senza parametri
    public void SwitchToEnglish()
    {
        canvaTablet.SetActive(false);
        canvaTabletInglese.SetActive(true);
    }
}
