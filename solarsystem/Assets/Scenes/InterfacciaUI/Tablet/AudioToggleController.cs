using UnityEngine;
using UnityEngine.UI;

public class AudioToggleController : MonoBehaviour
{
    // Riferimento al Toggle Audio
    public Toggle audioToggle;

    private void Start()
    {
        // Assicura che il metodo venga chiamato quando lo stato del toggle cambia
        if (audioToggle != null)
        {
            audioToggle.onValueChanged.AddListener(OnToggleAudioChanged);
        }
        else
        {
            Debug.LogError("Il Toggle Audio non è assegnato nello script.");
        }
    }

    // Metodo chiamato quando il toggle cambia
    public void OnToggleAudioChanged(bool isOn)
{
    // Trova tutti i componenti AudioSource nella scena
    AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

    // Abilita o disabilita l'audio in base allo stato del toggle
    foreach (AudioSource audioSource in audioSources)
    {
        audioSource.mute = !isOn;
    }

    // Disabilita l'AudioListener se il toggle è spento
    AudioListener audioListener = FindObjectOfType<AudioListener>();
    if (audioListener != null)
    {
        audioListener.enabled = isOn;
    }
}
}
