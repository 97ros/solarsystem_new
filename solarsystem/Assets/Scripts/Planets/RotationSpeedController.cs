using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider speedSlider;  // Riferimento allo slider UI
    public EllipticalOrbit1[] planets;  // Riferimento agli script di rotazione dei pianeti

    void Start()
    {
        // Imposta il valore iniziale dello slider (da 0 a 2)
        speedSlider.value = 0.5f;  // Un valore iniziale che non è né troppo lento né troppo veloce

        // Aggiungi il listener per lo slider
        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Funzione chiamata quando lo slider cambia valore
    void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider Value: " + value); // Aggiungi un log per verificare che lo slider funzioni

        // Modifica la velocità di rotazione per ogni pianeta, moltiplicando per il valore dello slider
        foreach (EllipticalOrbit1 planet in planets)
        {
            planet.UpdateOrbitSpeed(value);
        }
    }
}
