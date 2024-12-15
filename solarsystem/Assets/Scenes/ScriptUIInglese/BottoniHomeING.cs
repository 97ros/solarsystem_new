using UnityEngine;

public class BottoniHomeING : MonoBehaviour
{
    // Riferimento al GameObject UIiniziale
    public GameObject UIinizialeING;

    public void GoHome()
    {
        // Attiva UIinizialeING
        UIinizialeING.SetActive(true);

        // Disattiva il genitore dell'oggetto corrente (la UI che contiene il bottone)
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
