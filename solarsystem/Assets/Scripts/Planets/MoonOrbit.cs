using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    [SerializeField]
    private Transform earth; // Oggetto Terra attorno al quale orbita la Luna

    [SerializeField]
    private float orbitSpeed = 5.0f; // Velocità angolare in gradi al secondo (scalata)

    private Vector3 initialOffset; // Posizione iniziale della Luna rispetto alla Terra
    private float currentAngle = 0f; // Angolo attuale dell'orbita in gradi

    void Start()
    {
        // Calcola l'offset iniziale (posizione iniziale della Luna rispetto alla Terra)
        if (earth != null)
        {
            initialOffset = transform.position - earth.position;
        }
    }

    void Update()
    {
        if (earth != null)
        {
            // Incrementa l'angolo corrente basato sulla velocità e sul tempo
            currentAngle += orbitSpeed * Time.deltaTime;
            currentAngle %= 360; // Mantiene l'angolo tra 0 e 360 gradi

            // Calcola la nuova posizione orbitale basata sull'angolo corrente
            float radians = currentAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians) * initialOffset.magnitude;
            float z = Mathf.Sin(radians) * initialOffset.magnitude;

            // Aggiorna la posizione della Luna
            transform.position = new Vector3(
                earth.position.x + x,
                earth.position.y + initialOffset.y, // Mantieni la distanza verticale invariata
                earth.position.z + z
            );
        }
    }
}
