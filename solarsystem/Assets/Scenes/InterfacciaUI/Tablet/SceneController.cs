using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class SceneController : MonoBehaviour
{
    private GameObject spaceShip;
    private PlayerSpaceship playerSpaceship;
    private ParticleSystem sunParticleSystem;

    void Start()
    {
        // Carica la scena "SampleScene" in modalità additive
       StartCoroutine(LoadSampleScene());
    }

    private IEnumerator LoadSampleScene()
    {
        // Carica la scena additive
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Assicurati che la scena sia attiva
        Scene sampleScene = SceneManager.GetSceneByName("SampleScene");
        if (sampleScene.IsValid() && sampleScene.isLoaded)
        {
            SceneManager.SetActiveScene(sampleScene);

            // Disattiva EventSystem nella scena secondaria
            DisableEventSystem(sampleScene);

            // Configura gli oggetti della scena
            SetupSampleSceneObjects();
        }
        else
        {
            Debug.LogError("SampleScene non valida o non caricata correttamente.");
        }
    }

    private void DisableEventSystem(Scene scene)
    {
        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            EventSystem eventSystem = obj.GetComponent<EventSystem>();
            if (eventSystem != null)
            {
                obj.SetActive(false);
                Debug.Log("EventSystem disattivato nella scena secondaria.");
                return;
            }
        }

        Debug.LogWarning("Nessun EventSystem trovato nella scena secondaria.");
    }

    private void SetupSampleSceneObjects()
    {
        // Trova l'oggetto SpaceShip
        spaceShip = GameObject.Find("SpaceShip");
        if (spaceShip != null)
        {
            spaceShip.SetActive(false); // Disattiva subito SpaceShip
            playerSpaceship = spaceShip.GetComponent<PlayerSpaceship>();
            if (playerSpaceship != null)
            {
                playerSpaceship.SetControls(false); // Disattiva i controlli
            }

            Debug.Log("SpaceShip trovata e configurata.");
        }
        else
        {
            Debug.LogWarning("SpaceShip non trovata nella scena SampleScene.");
        }

        // Trova il Particle System
        Transform particleSystemTransform = GameObject.Find("SolarSystem/mainSolarSystem/Sole/Sun/Particle System")?.transform;
        if (particleSystemTransform != null)
        {
            sunParticleSystem = particleSystemTransform.GetComponent<ParticleSystem>();
            if (sunParticleSystem != null)
            {
                sunParticleSystem.Stop(); // Disattiva il Particle System
                Debug.Log("Particle System configurato.");
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

    public void EnterTabletMode()
    {
        if (playerSpaceship != null)
        {
            playerSpaceship.SetControls(false);
        }

        if (sunParticleSystem != null)
        {
            sunParticleSystem.Stop();
        }

        if (spaceShip != null)
        {
            spaceShip.SetActive(false);
        }

        Debug.Log("Entrata nella modalità tablet.");
    }

    public void ExitTabletMode()
    {
        if (playerSpaceship != null)
        {
            playerSpaceship.SetControls(true);
        }

        if (sunParticleSystem != null)
        {
            sunParticleSystem.Play();
        }

        if (spaceShip != null)
        {
            spaceShip.SetActive(true);
        }

        Debug.Log("Ritorno alla modalità esplorazione.");
    }
}