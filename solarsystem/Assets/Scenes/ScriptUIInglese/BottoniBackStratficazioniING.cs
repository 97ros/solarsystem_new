using UnityEngine;

public class BottoniBackStratificazioniING : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIinformazioniING;

    // Metodo da assegnare al bottone Tablet
    public void OnTabletButtonClick()
    {
        // Disattiva il GameObject parent (la scena corrente)
        transform.parent.gameObject.SetActive(false);

        // Attiva UIiniziale
        if (UIinformazioniING != null)
        {
            UIinformazioniING.SetActive(true);
        }
        else
        {
            Debug.LogError("UIinizialeING non è assegnato nello script BottoniBackStratificazioniING.");
        }
    }
}
