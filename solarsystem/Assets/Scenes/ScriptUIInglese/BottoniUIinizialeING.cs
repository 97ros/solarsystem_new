using UnityEngine;

public class BottoniUIinizialeING : MonoBehaviour
{
    public GameObject UIinizialeING;
    public GameObject UIinformazioniING;
    public GameObject UIleggiING;
    public GameObject UIimpostazioniING;

    // Metodo da chiamare quando si clicca "InformazioniPianetiING"
    public void ShowInformazioniPianeti()
    {
        UIinizialeING.SetActive(false);
        UIinformazioniING.SetActive(true);
    }

    // Metodo da chiamare quando si clicca "LeggiING"
    public void ShowLeggi()
    {
        UIinizialeING.SetActive(false);
        UIleggiING.SetActive(true);
    }

    // Metodo da chiamare quando si clicca "ImpostazioniING"
    public void ShowImpostazioni()
    {
        UIinizialeING.SetActive(false);
        UIimpostazioniING.SetActive(true);
    }
}
