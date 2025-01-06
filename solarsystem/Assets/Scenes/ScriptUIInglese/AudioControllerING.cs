using UnityEngine;
using UnityEngine.UI;

public class AudioControllerING : MonoBehaviour
{
    // Riferimenti ai Toggle Audio nei rispettivi canvas
    public Toggle audioToggleIT;  // Toggle per CanvaTablet (italiano)
    public Toggle audioToggleEN;  // Toggle per CanvaTabletING (inglese)

    private void Start()
    {
        // Verifica che i toggle siano assegnati
        if (audioToggleIT != null && audioToggleEN != null)
        {
            // Aggiunge listener per monitorare i cambiamenti di stato dei toggle
            audioToggleIT.onValueChanged.AddListener(OnToggleAudioChangedIT);
            audioToggleEN.onValueChanged.AddListener(OnToggleAudioChangedEN);

            // Imposta lo stato iniziale dei toggle in base a quello di uno dei due
            SyncToggles(audioToggleIT.isOn);
        }
        else
        {
            Debug.LogError("I Toggle Audio non sono assegnati correttamente nello script.");
        }
    }

    // Metodo per sincronizzare entrambi i toggle
    private void SyncToggles(bool isOn)
    {
        audioToggleIT.isOn = isOn; // Imposta il toggle italiano
        audioToggleEN.isOn = isOn; // Imposta il toggle inglese

        // Chiamata per attivare/disattivare l'audio
        OnToggleAudioChanged(isOn);
    }

    // Metodo chiamato quando cambia lo stato del toggle italiano
    private void OnToggleAudioChangedIT(bool isOn)
    {
        // Sincronizza lo stato dell'altro toggle (inglese)
        audioToggleEN.isOn = isOn;
        
        // Chiamata per attivare/disattivare l'audio
        OnToggleAudioChanged(isOn);
    }

    // Metodo chiamato quando cambia lo stato del toggle inglese
    private void OnToggleAudioChangedEN(bool isOn)
    {
        // Sincronizza lo stato dell'altro toggle (italiano)
        audioToggleIT.isOn = isOn;
        
        // Chiamata per attivare/disattivare l'audio
        OnToggleAudioChanged(isOn);
    }

    // Metodo per attivare/disattivare l'audio in base allo stato del toggle
    public void OnToggleAudioChanged(bool isOn)
    {
        // Trova tutti gli AudioSource nella scena
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Attiva o disattiva tutti gli AudioSource
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.enabled = isOn; // Abilita o disabilita il componente AudioSource
        }

        // Modifica il volume globale (AudioListener)
        AudioListener.volume = isOn ? 1f : 0f; // Volume 0 quando è spento, 1 quando è acceso

        Debug.Log($"Audio {(isOn ? "attivato" : "disattivato")} per {audioSources.Length} AudioSource nella scena.");
    }
}