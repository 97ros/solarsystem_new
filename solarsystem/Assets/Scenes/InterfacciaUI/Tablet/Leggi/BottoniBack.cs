using UnityEngine;

public class BottoniBack : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIleggi;

    // Metodo da assegnare al bottone Tablet
    public void OnButtonClick()
    {
        // Disattiva il GameObject parent (la scena corrente)
        transform.parent.gameObject.SetActive(false);

        // Attiva UIiniziale
        if (UIleggi != null)
        {
            UIleggi.SetActive(true);
        }
        else
        {
            Debug.LogError("UIleggi non è assegnato nello script TabletButtonHandler.");
        }
    }
}