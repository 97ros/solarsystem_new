using UnityEngine;
using UnityEngine.UI;

public class AudioToggleControllerING : MonoBehaviour
{
    // Riferimento al Toggle Audio
    public Toggle audioToggle;

    private void Start()
    {
        // Controlla che il Toggle sia assegnato
        if (audioToggle != null)
        {
            // Aggiunge un listener per rilevare i cambiamenti nello stato del toggle
            audioToggle.onValueChanged.AddListener(OnToggleAudioChanged);

            // Imposta lo stato iniziale in base al Toggle
            OnToggleAudioChanged(audioToggle.isOn);
        }
        else
        {
            Debug.LogError("Il Toggle Audio non è assegnato nello script.");
        }
    }

    // Metodo chiamato quando lo stato del toggle cambia
    public void OnToggleAudioChanged(bool isOn)
{
    // Trova tutti gli AudioSource nella scena
    AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

    // Disattiva o attiva tutti gli AudioSource
    foreach (AudioSource audioSource in audioSources)
    {
        audioSource.enabled = isOn; // Abilita o disabilita il componente AudioSource
    }

    // Disattiva il volume globale anziché l'AudioListener
    AudioListener.volume = isOn ? 1f : 0f; // Volume 0 quando è spento, 1 quando è acceso

    Debug.Log($"Audio {(isOn ? "attivato" : "disattivato")} per {audioSources.Length} AudioSource nella scena.");
}
}