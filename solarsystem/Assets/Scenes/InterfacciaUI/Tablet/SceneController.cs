using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Carica la scena "SampleScene" in modalità additive
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        // Aspetta un frame per assicurarti che la scena sia caricata prima di cercare gli oggetti
        StartCoroutine(DisattivaOggetti());
    }

    private System.Collections.IEnumerator DisattivaOggetti()
    {
        // Aspetta un frame per assicurarti che la scena sia completamente caricata
        yield return null;

        // Disattiva l'oggetto SpaceShip
        GameObject spaceShip = GameObject.Find("SpaceShip");
        if (spaceShip != null)
        {
            spaceShip.SetActive(false);
            Debug.Log("SpaceShip disattivata all'avvio del gioco.");
        }
        else
        {
            Debug.LogWarning("SpaceShip non trovata nella scena.");
        }

        // Trova il Particle System tramite il percorso nella gerarchia
        Transform particleSystemTransform = GameObject.Find("SolarSystem/mainSolarSystem/Sole/Sun/Particle System")?.transform;
        if (particleSystemTransform != null)
        {
            // Ottieni il componente ParticleSystem e ferma l'emissione
            ParticleSystem ps = particleSystemTransform.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(); // Ferma l'emissione delle particelle
                Debug.Log("Emissione del Particle System fermata all'avvio del gioco.");
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
}