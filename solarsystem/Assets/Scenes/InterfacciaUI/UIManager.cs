using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIiniziale;
    public GameObject UIleggi;
    public GameObject UIimpostazioni;
    public GameObject UIinformazioni;

    public GameObject EventSystem;
    public GameObject SpaceShip;
    public GameObject CanvaTablet;

    // Start is called before the first frame update
    void Start()
    {
        // Disattiva gli altri UI inizialmente
        UIiniziale.SetActive(true);
        UIleggi.SetActive(false);
        UIimpostazioni.SetActive(false);
        UIinformazioni.SetActive(false);

        // Disabilita EventSystem e SpaceShip
        EventSystem.SetActive(false);
        SpaceShip.SetActive(false);

        // Attiva il cursore del mouse su CanvaTablet
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Impostiamo il canvas a CanvaTablet
        CanvaTablet.SetActive(true);
    }

    // Funzione per attivare UIimpostazioni
    public void ShowImpostazioni()
    {
        UIiniziale.SetActive(false);
        UIleggi.SetActive(false);
        UIinformazioni.SetActive(false);
        UIimpostazioni.SetActive(true);
    }

    // Funzione per attivare UIleggi
    public void ShowLeggi()
    {
        UIiniziale.SetActive(false);
        UIleggi.SetActive(true);
        UIimpostazioni.SetActive(false);
        UIinformazioni.SetActive(false);
    }

    // Funzione per attivare UIinformazioni
    public void ShowInformazioni()
    {
        UIiniziale.SetActive(false);
        UIleggi.SetActive(false);
        UIimpostazioni.SetActive(false);
        UIinformazioni.SetActive(true);
    }
}

