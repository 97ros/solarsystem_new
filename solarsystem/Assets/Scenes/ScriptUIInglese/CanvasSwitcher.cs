using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject canvaTablet; // Canvas italiano
    public GameObject canvaTabletInglese; // Canvas inglese

    // Metodo per attivare CanvaTabletInglese e disattivare CanvaTablet
    public void SwitchToEnglish()
    {
        canvaTablet.SetActive(false);
        canvaTabletInglese.SetActive(true);
    }

    // Metodo per attivare CanvaTablet e disattivare CanvaTabletInglese
    public void SwitchToItalian()
    {
        canvaTablet.SetActive(true);
        canvaTabletInglese.SetActive(false);
    }
}
