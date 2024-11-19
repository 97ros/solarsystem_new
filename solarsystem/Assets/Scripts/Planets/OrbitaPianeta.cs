using UnityEngine;

public class OrbitaPianeta : MonoBehaviour
{
    public Transform sole; // Riferimento al Sole
    public float periodoOrbitale = 365.0f; // Periodo orbitale in giorni terrestri
    public float inclinazioneOrbitale = 0.0f; // Inclinazione orbitale in gradi

    private float velocitaAngolare;

    void Start()
    {
        // Calcola la velocità angolare in base al periodo orbitale
        velocitaAngolare = 360.0f / periodoOrbitale; // gradi al giorno
        
        // Applica l'inclinazione
        transform.rotation = Quaternion.Euler(inclinazioneOrbitale, 0, 0);
    }

    void Update()
    {
        // Ruota il pianeta attorno al Sole
        transform.RotateAround(sole.position, Vector3.up, velocitaAngolare * Time.deltaTime);
    }
}
