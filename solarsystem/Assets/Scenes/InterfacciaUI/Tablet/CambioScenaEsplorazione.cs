using UnityEngine;
using UnityEngine.SceneManagement;  // Necessario per gestire le scene
using UnityEngine.UI;  // Necessario per lavorare con UI

public class CambioScenaEsplorazione : MonoBehaviour
{
    // Funzione per caricare la scena "TabletUI"
    public void GoToScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
