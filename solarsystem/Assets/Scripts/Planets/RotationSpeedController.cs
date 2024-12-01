using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider speedSlider;  // Riferimento allo slider UI
    public EllipticalOrbit1[] planets;  // Riferimento agli script di rotazione dei pianeti

    void Start()
    {
        speedSlider.minValue = 0f;
        speedSlider.maxValue = 4f;
        speedSlider.value = 2f;  // Valore iniziale corrispondente alla velocità base

        // Aggiungi il listener per lo slider
        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Funzione chiamata quando lo slider cambia valore
    void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider Value: " + value);

        // Modifica la velocità di rotazione per ogni pianeta
        foreach (EllipticalOrbit1 planet in planets)
        {
            planet.UpdateOrbitSpeed(value);
        }
    }
}