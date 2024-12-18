using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject canvaTablet; // Canvas italiano
    public GameObject canvaTabletInglese; // Canvas inglese

    // Metodo per attivare CanvaTabletInglese e disattivare CanvaTablet
    public void SwitchToEnglish()
    {
        LanguageManager.SetLanguage(LanguageManager.Language.English); // Usa LanguageManager.Language
    }

    // Metodo per attivare CanvaTablet e disattivare CanvaTabletInglese
    public void SwitchToItalian()
    {
        LanguageManager.SetLanguage(LanguageManager.Language.Italian); // Usa LanguageManager.Language
    }
}