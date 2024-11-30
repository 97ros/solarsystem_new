using UnityEngine;

public class EllipticalOrbit1 : MonoBehaviour
{
    public Transform sun;  // Riferimento al Sole
    public float baseOrbitSpeed = 10f;  // Velocità di rotazione di base (gradi per secondo)
    public float orbitSpeed = 10f;  // Velocità di rotazione attuale (modificata dallo slider)
    public float semiMajorAxis = 10f;  // Semi-asse maggiore (raggio orizzontale dell'orbita)
    public float semiMinorAxis = 5f;  // Semi-asse minore (raggio verticale dell'orbita)

    private float angle = 0f;  // Angolo di rotazione

    void Start()
    {
        // Assicura che il pianeta inizi a un punto sull'orbita ellittica
    }

    void Update()
    {
        // Incrementa l'angolo per la rotazione orbitale
        angle += orbitSpeed * Time.deltaTime;

        // Converte l'angolo in radianti per la trigonometria
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calcola la posizione del pianeta lungo l'orbita ellittica
        float x = sun.position.x + semiMajorAxis * Mathf.Cos(angleInRadians);  // Posizione lungo l'asse X
        float y = sun.position.y + semiMinorAxis * Mathf.Sin(angleInRadians); // Posizione lungo l'asse Y
        float z = sun.position.z;  // Mantieni la componente Z costante

        // Imposta la nuova posizione del pianeta
        transform.position = new Vector3(x, y, z);
    }

    // Metodo per aggiornare la velocità dell'orbita tramite lo slider
    public void UpdateOrbitSpeed(float sliderValue)
    {
        // Se lo slider è a 0, ferma la rotazione
        if (sliderValue == 0)
        {
            orbitSpeed = 0;
        }
        else
        {
            // Calcola l'incremento proporzionale della velocità in base alla velocità iniziale e il range dello slider (da 0 a 2)
            orbitSpeed = baseOrbitSpeed * (1 + (sliderValue * 2));  // Lo slider va da 0 a 2, quindi incremento fino a 3 volte la velocità base
        }

        Debug.Log("Orbit Speed Updated: " + orbitSpeed); // Aggiungi un log per verificare il cambiamento della velocità
    }
}
