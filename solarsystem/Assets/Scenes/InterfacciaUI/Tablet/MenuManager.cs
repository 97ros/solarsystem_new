using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject UIiniziale;
    public GameObject UIinformazioni;
    public GameObject UIimpostazioni;
    public GameObject UIleggi;

    void Start()
    {
        // All'avvio della scena, attiva solo UIiniziale e disattiva gli altri
        ActivateMenu(UIiniziale);
    }

    public void ShowUIinformazioni()
    {
        ActivateMenu(UIinformazioni);
    }

    public void ShowUIimpostazioni()
    {
        ActivateMenu(UIimpostazioni);
    }

    public void ShowUIleggi()
    {
        ActivateMenu(UIleggi);
    }

    public void ReturnToUIiniziale()
    {
        ActivateMenu(UIiniziale);
    }

    private void ActivateMenu(GameObject activeMenu)
    {
        // Disattiva tutti i Canvas
        UIiniziale.SetActive(false);
        UIinformazioni.SetActive(false);
        UIimpostazioni.SetActive(false);
        UIleggi.SetActive(false);

        // Attiva il Canvas desiderato
        activeMenu.SetActive(true);
    }
}