using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    public Transform sun;  // Il Sole attorno a cui orbita il pianeta
    public float semiMajorAxis = 10f;  // Semi-asse maggiore dell'orbita
    public float semiMinorAxis = 5f;  // Semi-asse minore dell'orbita
    public int resolution = 100;  // Numero di segmenti per disegnare l'orbita

    private LineRenderer lineRenderer;

    void Start()
    {
        // Ottieni il LineRenderer e configurane le proprietà
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution + 1;  // +1 per chiudere il cerchio
        lineRenderer.loop = true;  // Chiudi l'orbita
        lineRenderer.useWorldSpace = true;  // Usa il sistema di coordinate globale

        // Disegna l'orbita
        DrawOrbit();
    }

    void DrawOrbit()
    {
        Vector3[] orbitPoints = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            // Calcola l'angolo corrente (diviso uniformemente lungo l'orbita)
            float angle = i * 2 * Mathf.PI / resolution;

            // Calcola la posizione X e Y lungo l'ellisse
            float x = semiMajorAxis * Mathf.Cos(angle);
            float y = semiMinorAxis * Mathf.Sin(angle);

            // Aggiungi la posizione del Sole per ottenere la posizione globale
            orbitPoints[i] = new Vector3(sun.position.x + x, sun.position.y + y, sun.position.z);
        }

        // Imposta i punti calcolati nel LineRenderer
        lineRenderer.SetPositions(orbitPoints);
    }
}
