using UnityEngine;

public class BottoniLeggiING : MonoBehaviour
{
    // Riferimenti agli oggetti Stratificazione
    public GameObject PrimaLeggediKepleroING;
    public GameObject SecondaLeggediKepleroING;
    public GameObject TerzaLeggediKepleroING;
    public GameObject LeggediGravitàING;
    
    // Riferimento a UIinformazioni
    public GameObject UIleggiING;

    // Metodo per attivare un oggetto e disattivare tutti gli altri
    public void ShowLeggi(GameObject leggiDaAttivare)
    {
        // Disattiva tutti gli oggetti di stratificazione
        PrimaLeggediKepleroING.SetActive(false);
        SecondaLeggediKepleroING.SetActive(false);
        TerzaLeggediKepleroING.SetActive(false);
        LeggediGravitàING.SetActive(false);

        // Disattiva UIinformazioni
        UIleggiING.SetActive(false);

        // Attiva solo l'oggetto passato come parametro
        leggiDaAttivare.SetActive(true);
    }

    // Questi metodi vengono collegati ai bottoni nell'Inspector
    public void OnButtonPrimaLeggediKepleroINGClick() => ShowLeggi(PrimaLeggediKepleroING);
    public void OnButtonSecondaLeggediKepleroINGClick() => ShowLeggi(SecondaLeggediKepleroING);
    public void OnButtonTerzaLeggediKepleroINGClick() => ShowLeggi(TerzaLeggediKepleroING);
    public void OnButtonLeggediGravitàINGClick() => ShowLeggi(LeggediGravitàING);
    
}
