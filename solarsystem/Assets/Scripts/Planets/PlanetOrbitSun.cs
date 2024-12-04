using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    [SerializeField]
    private Transform sun; // Oggetto Sole attorno al quale ruotano i pianeti

    [SerializeField]
    private float orbitSpeed = 10.0f; // Velocità angolare dell'orbita in gradi al secondo

    void Update()
    {
        if (sun != null)
        {
            // Ruota attorno al Sole sull'asse Y
            transform.RotateAround(sun.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
