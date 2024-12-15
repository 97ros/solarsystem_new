using UnityEngine;

public class BottoniBackLeggiING : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIleggiING;

    // Metodo da assegnare al bottone Tablet
    public void OnButtonClick()
    {
        // Disattiva il GameObject parent (la scena corrente)
        transform.parent.gameObject.SetActive(false);

        // Attiva UIiniziale
        if (UIleggiING != null)
        {
            UIleggiING.SetActive(true);
        }
        else
        {
            Debug.LogError("UIleggiING non è assegnato nello script BottoniBackING.");
        }
    }
}