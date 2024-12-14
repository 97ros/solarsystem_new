using UnityEngine;

public class DisableScriptsOnTablet : MonoBehaviour
{
    // Riferimento all'oggetto CanvasTablet
    public GameObject canvasTablet;

    void Update()
    {
        // Verifica se CanvasTablet è attivo
        if (canvasTablet.activeInHierarchy)
        {
            // Disattiva gli script su tutti gli oggetti con PlanetSelfRotation e PlanetOrbit
            DisablePlanetScripts();
        }
        else
        {
            // Puoi decidere se fare altre operazioni quando CanvasTablet non è attivo
        }
    }

    void DisablePlanetScripts()
    {
        // Trova tutti gli oggetti nella scena
        PlanetSelfRotation[] selfRotationScripts = FindObjectsOfType<PlanetSelfRotation>();
        PlanetOrbit[] orbitScripts = FindObjectsOfType<PlanetOrbit>();

        // Disattiva PlanetSelfRotation su ogni oggetto
        foreach (PlanetSelfRotation script in selfRotationScripts)
        {
            script.enabled = false;
        }

        // Disattiva PlanetOrbit su ogni oggetto
        foreach (PlanetOrbit script in orbitScripts)
        {
            script.enabled = false;
        }
    }
}
