using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Velocità di rotazione (modificabile dall'Inspector)
    public Vector3 rotationSpeed = new Vector3(0, 10, 0);

    void Update()
    {
        // Ruota l'oggetto attorno ai suoi assi locali
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
