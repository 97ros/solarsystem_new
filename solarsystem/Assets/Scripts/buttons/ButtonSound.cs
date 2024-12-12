using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    // Variabile pubblica per assegnare un suono dall'Inspector
    public AudioClip soundClip;

    // Riferimento all'AudioSource
    private AudioSource audioSource;
    

    void Start()
    {
        // Ottieni o aggiungi un AudioSource al GameObject
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura l'AudioSource
        audioSource.playOnAwake = false; // Non riprodurre automaticamente
        audioSource.clip = soundClip;   // Assegna il suono scelto
        audioSource.priority = 0; // Massima priorità
    }

    // Metodo per riprodurre il suono
    public void PlaySound()
    {
        Debug.LogWarning("suono");
        audioSource.PlayOneShot(soundClip);
    }
}
