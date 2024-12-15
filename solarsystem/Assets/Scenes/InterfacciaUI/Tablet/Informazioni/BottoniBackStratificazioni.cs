using UnityEngine;

public class BottoniBackStratificazioni : MonoBehaviour
{
    // Riferimento all'oggetto UIinformazioni
    public GameObject UIinformazioni;

    // Metodo da assegnare al pulsante Back
    public void OnBackButtonPressed()
    {
        // Disattiva l'oggetto corrente
        gameObject.transform.parent.gameObject.SetActive(false);

        // Attiva l'oggetto UIinformazioni
        if (UIinformazioni != null)
        {
            UIinformazioni.SetActive(true);
        }
        else
        {
            Debug.LogWarning("UIinformazioni non è assegnato!");
        }
    }
}
