using UnityEngine;

public class UIController : MonoBehaviour
{
    // Riferimenti agli oggetti UI
    public GameObject UIiniziale;
    public GameObject UIleggi;
    public GameObject UIimpostazioni;
    public GameObject UIinformazioni;

    // Metodo che attiva l'UI iniziale e disattiva le altre UI
    void Start()
    {
        // Disattiva tutte le UI all'inizio
        DisattivaTutteLeUI();

        // Attiva solo l'UI iniziale in base alla lingua
        if (LanguageManager.CurrentLanguage == LanguageManager.Language.Italian) // Usa LanguageManager.Language
        {
            UIiniziale.SetActive(true);
        }
        else
        {
            // Se la lingua è inglese, non fare nulla qui
            // L'UI iniziale inglese verrà attivata da SceneManagerController
        }
    }

    // Metodo che disattiva tutte le UI
    void DisattivaTutteLeUI()
    {
        UIleggi.SetActive(false);
        UIimpostazioni.SetActive(false);
        UIinformazioni.SetActive(false);
        UIiniziale.SetActive(false);
    }

    // Metodo per attivare UIinformazioni
    public void MostraInformazioniPianeti()
    {
        DisattivaTutteLeUI();
        UIinformazioni.SetActive(true);
    }

    // Metodo per attivare UIimpostazioni
    public void MostraImpostazioni()
    {
        DisattivaTutteLeUI();
        UIimpostazioni.SetActive(true);
    }

    // Metodo per attivare UIleggi
    public void MostraLeggi()
    {
        DisattivaTutteLeUI();
        UIleggi.SetActive(true);
    }

    // Metodo per attivare UIiniziale (eventualmente non necessario se vuoi mantenere UIiniziale visibile all'inizio)
    public void MostraUIiniziale()
    {
        DisattivaTutteLeUI();
        UIiniziale.SetActive(true);
    }
}