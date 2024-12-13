using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Slider loadingBar;  // Riferimento alla barra di caricamento (Slider)
    public GameObject loadingScreen;  // Oggetto UI per la barra di caricamento

    // Funzione da chiamare quando il pulsante "Inizio" viene premuto
    public void LoadSceneWithLoading()
    {
        // Mostra la barra di caricamento
        loadingScreen.SetActive(true);

        // Avvia il caricamento della scena in background
        StartCoroutine(LoadSceneAsync("TabletUI"));
    }

    // Coroutine per caricare la scena in background
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Assicurati che la scena non venga caricata in modo sincrono
        operation.allowSceneActivation = false;

        // Fino a quando la scena non è completamente caricata
        while (!operation.isDone)
        {
            // Aggiorna la barra di caricamento
            loadingBar.value = operation.progress;

            // Se il caricamento è completato, attiva la scena
            if (operation.progress >= 0.9f)
            {
                // Puoi impostare un valore di completamento di 1 per la barra
                loadingBar.value = 1f;

                // Attiva la scena una volta che è pronta
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
