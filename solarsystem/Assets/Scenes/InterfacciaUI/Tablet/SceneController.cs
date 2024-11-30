using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject spaceShip;
    private PlayerSpaceship playerSpaceship;

    private ParticleSystem sunParticleSystem;

    void Start()
    {
        // Carica la scena "SampleScene" in modalità additive
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        // Aspetta un frame per assicurarti che la scena sia caricata prima di cercare gli oggetti
        StartCoroutine(SetupScene());
    }


    private System.Collections.IEnumerator SetupScene()
    {
        yield return null;

        // Ottieni riferimento alla navicella
        spaceShip = GameObject.Find("SpaceShip");
        if (spaceShip != null)
        {
            playerSpaceship = spaceShip.GetComponent<PlayerSpaceship>();
            playerSpaceship?.SetControls(false);
        }
        else
        {
            Debug.LogWarning("SpaceShip non trovata nella scena.");
        }

        // Ottieni riferimento al Particle System
        Transform particleSystemTransform = GameObject.Find("SolarSystem/mainSolarSystem/Sole/Sun/Particle System")?.transform;
        if (particleSystemTransform != null)
        {
            sunParticleSystem = particleSystemTransform.GetComponent<ParticleSystem>();
            if (sunParticleSystem != null)
            {
                sunParticleSystem.Stop(); // Disattiva le particelle inizialmente
            }
            else
            {
                Debug.LogWarning("Il componente ParticleSystem non è stato trovato sull'oggetto.");
            }
        }
        else
        {
            Debug.LogWarning("Particle System non trovato con il percorso specificato.");
        }
    }

    public void DeactivateSpaceShip()
{
    if (spaceShip != null)
    {
        spaceShip.SetActive(false); // Disattiva l'oggetto SpaceShip
        Debug.Log("SpaceShip disattivata.");
    }
    else
    {
        Debug.LogWarning("Impossibile disattivare SpaceShip perché non è stata trovata.");
    }
}

    public void EnterTabletMode()
{
    if (playerSpaceship != null)
    {
        playerSpaceship.SetControls(false); // Disabilita i controlli
    }

    if (sunParticleSystem != null)
    {
        sunParticleSystem.Stop(); // Disattiva il Particle System
    }

    DeactivateSpaceShip(); // Disattiva l'oggetto SpaceShip

    Debug.Log("Entrata nella modalità tablet.");
}

    public void ExitTabletMode()
    {
        if (playerSpaceship != null)
        {
            playerSpaceship.SetControls(true); // Riabilita i controlli
        }

        if (sunParticleSystem != null)
        {
            sunParticleSystem.Play(); // Riattiva il Particle System
        }

        Debug.Log("Ritorno alla modalità esplorazione.");
    }
}
