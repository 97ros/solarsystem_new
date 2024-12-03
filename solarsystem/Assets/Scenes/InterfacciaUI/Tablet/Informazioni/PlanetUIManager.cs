using UnityEngine;
using UnityEngine.UI;

public class PlanetUIManager : MonoBehaviour
{
    // Array dei Canvas dei pianeti
    public GameObject[] planetCanvases;

    // Funzione per attivare il Canvas corretto
    public void ShowPlanetCanvas(int planetIndex)
    {
        // Disattiva tutti i Canvas
        foreach (GameObject canvas in planetCanvases)
        {
            canvas.SetActive(false);
        }

        // Attiva il Canvas selezionato
        if (planetIndex >= 0 && planetIndex < planetCanvases.Length)
        {
            planetCanvases[planetIndex].SetActive(true);
        }
    }
}