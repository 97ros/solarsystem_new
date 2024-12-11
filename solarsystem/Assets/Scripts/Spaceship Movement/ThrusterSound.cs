using UnityEngine;

public class ThrusterSound : MonoBehaviour
{
    public AudioSource thrusterSound; // L'AudioSource per il suono del propulsore
    private Rigidbody rb; // Per rilevare il movimento della navicella

    public float fadeSpeed = 5.0f; // Velocità di fade del volume
    private float targetVolume = 0f; // Volume desiderato
    private bool isThrusting = false; // Indica se i tasti di movimento sono premuti

    void Start()
    {
        // Ottieni il Rigidbody dell'oggetto
        rb = GetComponent<Rigidbody>();

        // Controlla che l'AudioSource sia assegnato
        if (thrusterSound == null)
        {
            Debug.LogError("ThrusterSound: AudioSource non assegnato!");
        }
        else
        {
            thrusterSound.volume = 0f; // Assicurati che il volume sia inizialmente a 0
        }
    }
void Update()
    {
    // Controlla gli input di movimento
        float verticalMove = Input.GetAxis("Vertical");
        float horizontalMove = Input.GetAxis("Horizontal");

        // Verifica se i tasti di movimento sono premuti
        isThrusting = Mathf.Abs(verticalMove) > 0.1f || Mathf.Abs(horizontalMove) > 0.1f;

        if (isThrusting)
        {
            targetVolume = 0.03f; // Imposta il volume target al massimo

            // Avvia il suono se non è già attivo
            if (!thrusterSound.isPlaying)
            {
                thrusterSound.Play();
            }
        }
        else
        {
            targetVolume = 0.0f; // Imposta il volume target a zero
        }

        // Gradualmente cambia il volume verso il target
        thrusterSound.volume = Mathf.Lerp(thrusterSound.volume, targetVolume, Time.deltaTime * fadeSpeed);

        // Ferma il suono completamente quando il volume è vicino a zero e i tasti non sono premuti
        if (!isThrusting && thrusterSound.volume < 0.01f)
        {
            thrusterSound.Stop();
            thrusterSound.volume = 0f; // Resetta il volume per sicurezza
        }
    }
}