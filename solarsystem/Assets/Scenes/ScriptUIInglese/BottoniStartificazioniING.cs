using UnityEngine;

public class BottoniStratificazioniING : MonoBehaviour
{
    // Riferimenti agli oggetti Stratificazione
    public GameObject StratificazioneMercurioING;
    public GameObject StratificazioneVenereING;
    public GameObject StratificazioneTerraING;
    public GameObject StratificazioneMarteING;
    public GameObject StratificazioneGioveING;
    public GameObject StratificazioneSaturnoING;
    public GameObject StratificazioneUranoING;
    public GameObject StratificazioneNettunoING;

    // Riferimento a UIinformazioni
    public GameObject UIinformazioniING;

    // Metodo per attivare un oggetto e disattivare tutti gli altri
    public void ShowStratificazione(GameObject stratificazioneDaAttivare)
    {
        // Disattiva tutti gli oggetti di stratificazione
        StratificazioneMercurioING.SetActive(false);
        StratificazioneVenereING.SetActive(false);
        StratificazioneTerraING.SetActive(false);
        StratificazioneMarteING.SetActive(false);
        StratificazioneGioveING.SetActive(false);
        StratificazioneSaturnoING.SetActive(false);
        StratificazioneUranoING.SetActive(false);
        StratificazioneNettunoING.SetActive(false);

        // Disattiva UIinformazioni
        UIinformazioniING.SetActive(false);

        // Attiva solo l'oggetto passato come parametro
        stratificazioneDaAttivare.SetActive(true);
    }

    // Questi metodi vengono collegati ai bottoni nell'Inspector
    public void OnButtonMercurioClick() => ShowStratificazione(StratificazioneMercurioING);
    public void OnButtonVenereClick() => ShowStratificazione(StratificazioneVenereING);
    public void OnButtonTerraClick() => ShowStratificazione(StratificazioneTerraING);
    public void OnButtonMarteClick() => ShowStratificazione(StratificazioneMarteING);
    public void OnButtonGioveClick() => ShowStratificazione(StratificazioneGioveING);
    public void OnButtonSaturnoClick() => ShowStratificazione(StratificazioneSaturnoING);
    public void OnButtonUranoClick() => ShowStratificazione(StratificazioneUranoING);
    public void OnButtonNettunoClick() => ShowStratificazione(StratificazioneNettunoING);
}
