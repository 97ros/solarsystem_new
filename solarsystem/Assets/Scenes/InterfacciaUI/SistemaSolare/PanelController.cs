using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Assegna il tuo Panel dal Canvas
    private bool isPanelShown = false;

    void Start()
    {
        if (!isPanelShown)
        {
            ShowPanel();
        }
    }

    void ShowPanel()
    {
        panel.SetActive(true); // Mostra il Panel
        isPanelShown = true;
        Invoke("HidePanel", 8f); // Nascondi il Panel dopo 8 secondi
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}
