using UnityEngine;

public class PlanetSelfRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f; // Velocità di rotazione in gradi al secondo

    void Update()
    {
        // Ruota il pianeta su sé stesso attorno all'asse Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
