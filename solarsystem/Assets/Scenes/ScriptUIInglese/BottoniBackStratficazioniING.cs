using UnityEngine;

public class BottoniBackStratificazioniING : MonoBehaviour
{
    // Riferimento all'oggetto UIinformazioni
    public GameObject UIinformazioniING;

    // Metodo da assegnare al pulsante Back
    public void OnBackButtonPressed()
    {
        // Disattiva l'oggetto corrente
        gameObject.transform.parent.gameObject.SetActive(false);

        // Attiva l'oggetto UIinformazioni
        if (UIinformazioniING != null)
        {
            UIinformazioniING.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UIinformazioniING non è assegnato!");
        }
    }
}
