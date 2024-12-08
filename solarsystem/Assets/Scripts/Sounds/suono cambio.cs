using UnityEngine;
using UnityEngine.SceneManagement;

public class StopSoundOnSceneChange : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Trova l'Audio Source sul GameObject corrente
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Nessun AudioSource trovato su " + gameObject.name);
        }

        // Associa il metodo OnSceneUnloaded all'evento di scena scaricata
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene current)
    {
        // Ferma l'audio quando la scena viene cambiata
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void OnDestroy()
    {
        // Rimuovi il metodo dall'evento quando l'oggetto viene distrutto
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
