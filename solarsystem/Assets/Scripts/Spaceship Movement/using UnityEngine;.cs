using UnityEngine;

public class ThrusterSound : MonoBehaviour
{
    public AudioSource thrusterSound; // L'AudioSource per il suono del propulsore
    private Rigidbody rb; // Per rilevare il movimento della navicella

    void Start()
    {
        // Ottieni il Rigidbody dell'oggetto
        rb = GetComponent<Rigidbody>();

        // Controlla che l'AudioSource sia assegnato
        if (thrusterSound == null)
        {
            Debug.LogError("ThrusterSound: AudioSource non assegnato!");
        }
    }

    void Update()
    {
        // Controlla se la navicella si sta muovendo
        if (rb.linearVelocity.magnitude > 0.1f) // Tolleranza per evitare falsi positivi
        {
            // Avvia il suono se non è già attivo
            if (!thrusterSound.isPlaying)
            {
                thrusterSound.Play();
            }
        }
        else
        {
            // Ferma il suono se la navicella è ferma
            if (thrusterSound.isPlaying)
            {
                thrusterSound.Stop();
            }
        }
    }
}
