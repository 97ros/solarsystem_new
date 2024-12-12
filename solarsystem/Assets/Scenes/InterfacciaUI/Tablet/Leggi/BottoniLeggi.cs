using UnityEngine;

public class BottoniLeggi : MonoBehaviour
{
    // Riferimenti agli oggetti Stratificazione
    public GameObject PrimaLeggediKeplero;
    public GameObject SecondaLeggediKeplero;
    public GameObject TerzaLeggediKeplero;
    public GameObject LeggediGravità;
    
    // Riferimento a UIinformazioni
    public GameObject UIleggi;

    // Metodo per attivare un oggetto e disattivare tutti gli altri
    public void ShowLeggi(GameObject leggiDaAttivare)
    {
        // Disattiva tutti gli oggetti di stratificazione
        PrimaLeggediKeplero.SetActive(false);
        SecondaLeggediKeplero.SetActive(false);
        TerzaLeggediKeplero.SetActive(false);
        LeggediGravità.SetActive(false);

        // Disattiva UIinformazioni
        UIleggi.SetActive(false);

        // Attiva solo l'oggetto passato come parametro
        leggiDaAttivare.SetActive(true);
    }

    // Questi metodi vengono collegati ai bottoni nell'Inspector
    public void OnButtonPrimaLeggediKepleroClick() => ShowLeggi(PrimaLeggediKeplero);
    public void OnButtonSecondaLeggediKepleroClick() => ShowLeggi(SecondaLeggediKeplero);
    public void OnButtonTerzaLeggediKepleroClick() => ShowLeggi(TerzaLeggediKeplero);
    public void OnButtonLeggediGravitàClick() => ShowLeggi(LeggediGravità);
    
}