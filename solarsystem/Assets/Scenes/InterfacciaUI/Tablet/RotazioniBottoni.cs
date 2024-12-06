using UnityEngine;

public class RotazioniBottoni : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Asse di rotazione
    [SerializeField] private float rotationSpeed = 10f;         // Velocità di rotazione

    // Update is called once per frame
    void Update()
    {
        // Ruota l'oggetto attorno al suo asse locale
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
