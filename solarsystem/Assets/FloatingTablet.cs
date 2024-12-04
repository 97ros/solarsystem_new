using UnityEngine;

public class FloatingTablet : MonoBehaviour
{
    public float amplitude = 0.2f; // L'intensità della fluttuazione
    public float frequency = 1f;  // La velocità della fluttuazione
    public Vector3 floatingDirection = Vector3.up; // Direzione della fluttuazione (es. su e giù)
    
    private Vector3 startPosition;

    void Start()
    {
        // Memorizza la posizione iniziale
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcola il nuovo offset
        Vector3 offset = floatingDirection * Mathf.Sin(Time.time * frequency) * amplitude;

        // Applica l'offset alla posizione iniziale
        transform.position = startPosition + offset;
    }
}
