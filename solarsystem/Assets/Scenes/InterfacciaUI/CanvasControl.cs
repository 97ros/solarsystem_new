using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    public GameObject canvaTablet;
    public GameObject canvaTabletInglese;
    
    private PlanetSelfRotation[] selfRotations;
    private PlanetOrbit[] planetOrbits;

    void Start()
    {
        // Trova tutti gli script nella scena
        selfRotations = FindObjectsOfType<PlanetSelfRotation>();
        planetOrbits = FindObjectsOfType<PlanetOrbit>();
    }

    void Update()
    {
        // Controlla se uno dei due canvas è attivo
        bool areCanvasesActive = canvaTablet.activeSelf || canvaTabletInglese.activeSelf;

        // Abilita/disabilita gli script in base allo stato dei canvas
        foreach (PlanetSelfRotation rotation in selfRotations)
        {
            rotation.enabled = !areCanvasesActive;
        }

        foreach (PlanetOrbit orbit in planetOrbits)
        {
            orbit.enabled = !areCanvasesActive;
        }
    }
}
