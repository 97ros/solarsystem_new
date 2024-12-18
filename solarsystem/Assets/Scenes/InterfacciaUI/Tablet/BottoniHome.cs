using UnityEngine;

public class BottoniHome : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIiniziale;
    
    public void GoHome()
    {
        // Attiva UIiniziale
        UIiniziale.SetActive(true);

        // Disattiva il genitore dell'oggetto corrente (la UI che contiene il bottone)
        gameObject.transform.parent.gameObject.SetActive(false);
        
    }

}
