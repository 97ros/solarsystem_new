using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen; // Riferimento alla schermata di caricamento
    public Slider progressBar; // Riferimento alla barra di progresso
    public Button myButton; // Assegna il pulsante nell'Inspector

    public void LoadSceneWrapper()
    {
        // Disattiva il pulsante cliccato
        myButton.gameObject.SetActive(false);

        // Assicurati di attivare la schermata di caricamento
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
            // Avvia la coroutine per il caricamento della scena
            StartCoroutine(LoadAsync("SampleScene"));
        }
        else
        {
            Debug.LogError("Il riferimento a LoadingScreen non è impostato!");
        }
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        // Inizia il caricamento della scena
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Aggiorna la barra di caricamento
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }
            else
            {
                Debug.LogWarning("Il riferimento a ProgressBar non è impostato!");
            }

            yield return null; // Aspetta il prossimo frame
        }
    }
}
