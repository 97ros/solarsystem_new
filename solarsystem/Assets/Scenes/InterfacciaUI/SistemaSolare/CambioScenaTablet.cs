using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScenaTablet : MonoBehaviour
{
    public GameObject panelMessage; // Riferimento al pannello
    private float timer = 10f;      // Durata del messaggio

    void Start()
    {
        // Mostra il pannello se siamo nella scena SampleScene
        if (SceneManager.GetActiveScene().name == "SampleScene" && panelMessage != null)
        {
            panelMessage.SetActive(true);
        }
    }

    void Update()
    {
        // Gestione pannello solo nella scena SampleScene
        if (SceneManager.GetActiveScene().name == "SampleScene" && panelMessage != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    panelMessage.SetActive(false); // Nascondi il pannello dopo 10 secondi
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
         {
           // Trova e distruggi il Canvas
           GameObject canvas = GameObject.Find("Canvas"); // Sostituisci "Canvas" con il nome esatto
              if (canvas != null)
              {
               Destroy(canvas); // Rimuove il Canvas dalla scena attuale
              }

            // Cambia scena verso TabletUI
             SceneManager.LoadScene("TabletUI");
          }

        
        
if (Input.GetKeyDown(KeyCode.T))
{
    if (panelMessage != null)
    {
        panelMessage.SetActive(false); // Nasconde il pannello prima del cambio scena
    }

    SceneManager.LoadScene("TabletUI");
}


    }
}