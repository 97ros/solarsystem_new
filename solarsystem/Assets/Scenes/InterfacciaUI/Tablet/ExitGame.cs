using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Metodo per uscire dall'applicazione
    public void QuitGame()
    {
        // Stampa un messaggio nella console durante lo sviluppo (utile per debugging)
        Debug.Log("Il gioco è stato chiuso!");

        // Uscire dall'applicazione
        Application.Quit();
    }
}
