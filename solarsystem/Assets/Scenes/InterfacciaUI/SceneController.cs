using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Carica la scena del tablet quando il gioco inizia
    void Start()
    {
        // Carica la scena del Tablet in modalità additiva
        SceneManager.LoadScene("Tablet", LoadSceneMode.Additive);
    }

    // Funzione per caricare o scaricare la scena del tablet
    public void ToggleTabletScene(bool isTabletActive)
    {
        if (isTabletActive)
        {
            // Carica la scena del tablet (se non è già caricato)
            SceneManager.LoadScene("Tablet", LoadSceneMode.Additive);
        }
        else
        {
            // Scarica la scena del tablet
            SceneManager.UnloadSceneAsync("Tablet");
        }
    }
}
