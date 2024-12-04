using UnityEngine;

public class PlanetUIManager : MonoBehaviour
{
    // Riferimenti agli oggetti Stratificazione
    public GameObject StratificazioneMercurio;
    public GameObject StratificazioneVenere;
    public GameObject StratificazioneTerra;
    public GameObject StratificazioneMarte;
    public GameObject StratificazioneGiove;
    public GameObject StratificazioneSaturno;
    public GameObject StratificazioneUrano;
    public GameObject StratificazioneNettuno;

    // Riferimento a UIinformazioni
    public GameObject UIinformazioni;

    // Metodo per attivare un oggetto e disattivare tutti gli altri
    public void ShowStratificazione(GameObject stratificazioneDaAttivare)
    {
        // Disattiva tutti gli oggetti di stratificazione
        StratificazioneMercurio.SetActive(false);
        StratificazioneVenere.SetActive(false);
        StratificazioneTerra.SetActive(false);
        StratificazioneMarte.SetActive(false);
        StratificazioneGiove.SetActive(false);
        StratificazioneSaturno.SetActive(false);
        StratificazioneUrano.SetActive(false);
        StratificazioneNettuno.SetActive(false);

        // Disattiva UIinformazioni
        UIinformazioni.SetActive(false);

        // Attiva solo l'oggetto passato come parametro
        stratificazioneDaAttivare.SetActive(true);
    }

    // Questi metodi vengono collegati ai bottoni nell'Inspector
    public void OnButtonMercurioClick() => ShowStratificazione(StratificazioneMercurio);
    public void OnButtonVenereClick() => ShowStratificazione(StratificazioneVenere);
    public void OnButtonTerraClick() => ShowStratificazione(StratificazioneTerra);
    public void OnButtonMarteClick() => ShowStratificazione(StratificazioneMarte);
    public void OnButtonGioveClick() => ShowStratificazione(StratificazioneGiove);
    public void OnButtonSaturnoClick() => ShowStratificazione(StratificazioneSaturno);
    public void OnButtonUranoClick() => ShowStratificazione(StratificazioneUrano);
    public void OnButtonNettunoClick() => ShowStratificazione(StratificazioneNettuno);
}