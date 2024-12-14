using UnityEngine;

public class BottoniBackStratificazioni : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIinformazioni;

    // Metodo da assegnare al bottone Tablet
    public void OnTabletButtonClick()
    {
        // Disattiva il GameObject parent (la scena corrente)
        transform.parent.gameObject.SetActive(false);

        // Attiva UIiniziale
        if (UIinformazioni != null)
        {
            UIinformazioni.SetActive(true);
        }
        else
        {
            Debug.LogError("UIiniziale non è assegnato nello script TabletButtonHandler.");
        }
    }
}
